using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using TheCharityBLL.DTOs.PaymentDTOs;
using TheCharityBLL.Services.Abstraction;
using TheCharityBLL.Services.Repository;

namespace TheCharityPL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        // Constructor
        private readonly IPaymobService _paymobService;
        private readonly ILogger<PaymentController> _logger;
        private readonly IConfiguration _configuration;

        public PaymentController(IPaymobService paymobService, ILogger<PaymentController> logger, IConfiguration configuration)
        {
            _paymobService = paymobService;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]decimal Amount)
        {
            if (Amount <= 0)
                return BadRequest(new { message = "Amount must be greater than zero." });

            var iframeUrl = await _paymobService.CreatePayment(Amount);

            return Ok(new { iframeUrl });
        }

        [HttpPost("callback")]
        [AllowAnonymous]  // Paymob has no JWT, must be anonymous
        public async Task<IActionResult> Callback([FromBody] PaymobCallbackWrapper wrapper)
        {
            try
            {
                // Log raw for debugging
                _logger.LogInformation("Paymob callback received. Type: {Type}", wrapper?.Type);

                if (wrapper?.Obj is null)
                {
                    _logger.LogWarning("Paymob callback wrapper or obj is null.");
                    return BadRequest(new { message = "Invalid callback data" });
                }

                var transaction = wrapper.Obj;

                // 1. Verify HMAC from query string
                var receivedHmac = Request.Query["hmac"].ToString();
                if (!VerifyHmac(transaction, receivedHmac))
                {
                    _logger.LogWarning("Invalid HMAC on callback for transaction {TransactionId}", transaction.Id);
                    return Unauthorized(new { message = "Invalid HMAC signature." });
                }

                // 2. Check if transaction was successful
                if (!transaction.Success)
                {
                    _logger.LogInformation("Payment failed for order {OrderId}. Transaction ID: {TransactionId}",
                        transaction.OrderId, transaction.Id);
                    return Ok(new { message = "Payment not successful.", status = "failed" });
                }

                // 3. Handle successful payment
                var amountInEgp = transaction.AmountCents / 100m;

                _logger.LogInformation(
                    "Payment successful. OrderId: {OrderId}, TransactionId: {TransactionId}, Amount: {Amount} {Currency}, Payment Method: {PaymentMethod}",
                    transaction.OrderId,
                    transaction.Id,
                    amountInEgp,
                    transaction.Currency ?? "EGP",
                    transaction.Order?.PaymentMethod ?? "Unknown");

                // 4. Extract additional payment details
                var paymentDetails = new
                {
                    CardType = transaction.Data?.CardType,
                    Last4Digits = transaction.SourceData?.Pan,
                    TransactionReference = transaction.Data?.ReceiptNo,
                    AuthorizationCode = transaction.Data?.AuthorizeId
                };

                _logger.LogInformation(
                    "Payment details - Card: {CardType}, Last4: {Last4}, Reference: {Reference}, AuthCode: {AuthCode}",
                    paymentDetails.CardType,
                    paymentDetails.Last4Digits,
                    paymentDetails.TransactionReference,
                    paymentDetails.AuthorizationCode);

                // TODO: Update your database here
                // await _orderService.MarkAsPaid(
                //     orderId: transaction.OrderId.ToString(),
                //     transactionId: transaction.Id.ToString(),
                //     amount: amountInEgp,
                //     paymentMethod: transaction.Order?.PaymentMethod,
                //     cardDetails: paymentDetails
                // );

                // 5. Return success (always return 200 to Paymob)
                return Ok(new
                {
                    message = "Callback processed successfully.",
                    transaction_id = transaction.Id,
                    order_id = transaction.OrderId,
                    status = "success"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing Paymob callback");
                // Always return 200 to Paymob even on error, but log it
                return Ok(new { message = "Callback received but processing failed", status = "error" });
            }
        }

        private bool VerifyHmac(PaymobTransaction transaction, string receivedHmac)
        {
            var secret = _configuration["Paymob:HmacKey"];
            if (string.IsNullOrEmpty(secret) || string.IsNullOrEmpty(receivedHmac))
            {
                _logger.LogWarning("HMAC verification failed: missing secret or received HMAC");
                return false;
            }

            try
            {
                // Build the string according to Paymob's specification
                var data = string.Concat(
                    transaction.AmountCents,
                    transaction.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss.ffffff"),
                    transaction.Currency,
                    transaction.ErrorOccured.ToString().ToLowerInvariant(),
                    transaction.HasParentTransaction.ToString().ToLowerInvariant(),
                    transaction.Id,
                    transaction.IntegrationId,
                    transaction.Is3dSecure.ToString().ToLowerInvariant(),
                    transaction.IsAuth.ToString().ToLowerInvariant(),
                    transaction.IsCapture.ToString().ToLowerInvariant(),
                    transaction.IsRefunded.ToString().ToLowerInvariant(),
                    transaction.IsStandalonePayment.ToString().ToLowerInvariant(),
                    transaction.IsVoided.ToString().ToLowerInvariant(),
                    transaction.Order?.Id ?? 0,
                    transaction.Owner,
                    transaction.Pending.ToString().ToLowerInvariant(),
                    transaction.SourceData?.Pan ?? "",
                    transaction.SourceData?.SubType ?? "",
                    transaction.SourceData?.Type ?? "",
                    transaction.Success.ToString().ToLowerInvariant()
                );

                _logger.LogDebug("HMAC data string: {Data}", data);

                using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(secret));
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                var computed = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();

                var isValid = computed == receivedHmac.ToLowerInvariant();

                if (!isValid)
                {
                    _logger.LogWarning("HMAC mismatch. Computed: {Computed}, Received: {Received}",
                        computed, receivedHmac.ToLowerInvariant());
                }

                return isValid;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error computing HMAC");
                return false;
            }
        }

    }
}
