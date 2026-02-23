using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TheCharityBLL.DTOs.PaymentDTOs
{
    public class PaymentKeyClaims
    {
        [JsonPropertyName("exp")]
        public long Exp { get; set; }

        [JsonPropertyName("extra")]
        public Dictionary<string, object>? Extra { get; set; }

        [JsonPropertyName("pmk_ip")]
        public string? PmkIp { get; set; }

        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("order_id")]
        public long OrderId { get; set; }

        [JsonPropertyName("amount_cents")]
        public long AmountCents { get; set; }

        [JsonPropertyName("billing_data")]
        public BillingData? BillingData { get; set; }

        [JsonPropertyName("integration_id")]
        public long IntegrationId { get; set; }

        [JsonPropertyName("lock_order_when_paid")]
        public bool LockOrderWhenPaid { get; set; }

        [JsonPropertyName("single_payment_attempt")]
        public bool SinglePaymentAttempt { get; set; }
    }
}