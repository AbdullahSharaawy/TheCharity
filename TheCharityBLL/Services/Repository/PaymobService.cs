using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TheCharityBLL.Services.Abstraction;

namespace TheCharityBLL.Services.Repository
{
    public class PaymobService : IPaymobService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PaymobService> _logger;
        private readonly string _apiKey;
        private readonly string _integrationId;
        private readonly string _iframeId;

        public PaymobService(IConfiguration config, ILogger<PaymobService> logger)
        {
            _httpClient = new HttpClient();
            _logger = logger;
            _apiKey = config["Paymob:ApiKey"] ?? throw new ArgumentNullException("Paymob:ApiKey is missing in config");
            _integrationId = config["Paymob:IntegrationId"] ?? throw new ArgumentNullException("Paymob:IntegrationId is missing in config");
            _iframeId = config["Paymob:IframeId"] ?? throw new ArgumentNullException("Paymob:IframeId is missing in config");
        }

        public async Task<string> CreatePayment(decimal amount, string currency = "EGP")
        {
            // ── Step 1: Authentication ──────────────────────────────────────
            var authBody = JsonSerializer.Serialize(new { api_key = _apiKey });

            var authResponse = await _httpClient.PostAsync(
                "https://accept.paymob.com/api/auth/tokens",
                new StringContent(authBody, Encoding.UTF8, "application/json")
            );

            var authContent = await authResponse.Content.ReadAsStringAsync();
            _logger.LogInformation("Auth response: {Content}", authContent);

            if (!authResponse.IsSuccessStatusCode)
                throw new Exception($"Paymob auth failed ({authResponse.StatusCode}): {authContent}");

            var authJson = JsonDocument.Parse(authContent).RootElement;

            if (!authJson.TryGetProperty("token", out var authTokenEl))
                throw new Exception($"Paymob auth response missing 'token'. Response: {authContent}");

            var authToken = authTokenEl.GetString()!;

            // ── Step 2: Create Order ────────────────────────────────────────
            var orderBody = JsonSerializer.Serialize(new
            {
                auth_token = authToken,          // ← required in body
                amount_cents = (int)(amount * 100),
                currency,
                delivery_needed = false,         // ← bool not string
                items = Array.Empty<object>()
            });

            // Clear previous headers to avoid conflicts
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", authToken);

            var orderResponse = await _httpClient.PostAsync(
                "https://accept.paymob.com/api/ecommerce/orders",
                new StringContent(orderBody, Encoding.UTF8, "application/json")
            );

            var orderContent = await orderResponse.Content.ReadAsStringAsync();
            _logger.LogInformation("Order response: {Content}", orderContent);

            if (!orderResponse.IsSuccessStatusCode)
                throw new Exception($"Paymob order creation failed ({orderResponse.StatusCode}): {orderContent}");

            var orderJson = JsonDocument.Parse(orderContent).RootElement;

            if (!orderJson.TryGetProperty("id", out var orderIdEl))
                throw new Exception($"Paymob order response missing 'id'. Response: {orderContent}");

            var orderId = orderIdEl.GetInt64();

            // ── Step 3: Payment Key ─────────────────────────────────────────
            var paymentBody = JsonSerializer.Serialize(new
            {
                auth_token = authToken,
                amount_cents = (int)(amount * 100),
                expiration = 3600,
                order_id = orderId,
                currency,
                integration_id = int.Parse(_integrationId),
                billing_data = new
                {
                    first_name = "Customer",
                    last_name = "Name",
                    email = "customer@example.com",
                    phone_number = "+201000000000",
                    apartment = "NA",
                    floor = "NA",
                    street = "NA",
                    building = "NA",
                    shipping_method = "NA",
                    postal_code = "NA",
                    city = "NA",
                    country = "EG",
                    state = "NA"
                }
            });

            var paymentResponse = await _httpClient.PostAsync(
                "https://accept.paymob.com/api/acceptance/payment_keys",
                new StringContent(paymentBody, Encoding.UTF8, "application/json")
            );

            var paymentContent = await paymentResponse.Content.ReadAsStringAsync();
            _logger.LogInformation("Payment key response: {Content}", paymentContent);

            if (!paymentResponse.IsSuccessStatusCode)
                throw new Exception($"Paymob payment key failed ({paymentResponse.StatusCode}): {paymentContent}");

            var paymentJson = JsonDocument.Parse(paymentContent).RootElement;

            if (!paymentJson.TryGetProperty("token", out var paymentTokenEl))
                throw new Exception($"Paymob payment key response missing 'token'. Response: {paymentContent}");

            var paymentKey = paymentTokenEl.GetString()!;

            // ── Step 4: Return iFrame URL ───────────────────────────────────
            return $"https://accept.paymob.com/api/acceptance/iframes/{_iframeId}?payment_token={paymentKey}";
        }
    }
}