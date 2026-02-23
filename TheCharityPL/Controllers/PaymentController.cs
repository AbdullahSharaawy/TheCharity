using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Callback([FromBody] PaymobCallbackRequest response)
        {
            if (response is null)
                return BadRequest();

            // 1. Verify HMAC
            var isValid = VerifyHmac(response);
            if (!isValid)
            {
                _logger.LogWarning("Paymob callback received with invalid HMAC.");
                return Unauthorized(new { message = "Invalid HMAC signature." });
            }

            // 2. Check transaction success
            if (!response.Success)
            {
                _logger.LogInformation("Payment failed for order {OrderId}.", response.OrderId);
                return Ok(new { message = "Payment not successful, no action taken." });
            }

            // 3. Handle successful payment (e.g. update booking/order status)
          //  await _paymobService.HandleSuccessfulPayment(response.OrderId, response.TransactionId);

            _logger.LogInformation("Payment successful for order {OrderId}, transaction {TransactionId}.",
                response.OrderId, response.TransactionId);

            return Ok(new { message = "Callback processed successfully." });
        }

        private bool VerifyHmac(PaymobCallbackRequest response)
        {
            // Paymob HMAC is computed from specific fields in a defined order
            var hmacSecret = _configuration["Paymob:HmacSecret"];

            var dataString = string.Concat(
                response.AmountCents,
                response.CreatedAt,
                response.Currency,
                response.ErrorOccurred,
                response.HasParentTransaction,
                response.OrderId,
                response.Owner,
                response.Pending,
                response.SourceDataPan,
                response.SourceDataSubType,
                response.SourceDataType,
                response.Success
            );

            using var hmac = new System.Security.Cryptography.HMACSHA512(
                System.Text.Encoding.UTF8.GetBytes(hmacSecret)
            );

            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dataString));
            var computedHmac = Convert.ToHexString(computedHash).ToLower();

            return computedHmac == response.Hmac;
        }
    }
}
