using System;
using System.Text.Json.Serialization;

namespace TheCharityBLL.DTOs.PaymentDTOs
{
    public class MigsOrder
    {
        [JsonPropertyName("acceptPartialAmount")]
        public bool AcceptPartialAmount { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("authenticationStatus")]
        public string? AuthenticationStatus { get; set; }

        [JsonPropertyName("chargeback")]
        public Chargeback? Chargeback { get; set; }

        [JsonPropertyName("creationTime")]
        public DateTime CreationTime { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("lastUpdatedTime")]
        public DateTime LastUpdatedTime { get; set; }

        [JsonPropertyName("merchantAmount")]
        public decimal MerchantAmount { get; set; }

        [JsonPropertyName("merchantCategoryCode")]
        public string? MerchantCategoryCode { get; set; }

        [JsonPropertyName("merchantCurrency")]
        public string? MerchantCurrency { get; set; }

        [JsonPropertyName("reference")]
        public string? Reference { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("totalAuthorizedAmount")]
        public decimal TotalAuthorizedAmount { get; set; }

        [JsonPropertyName("totalCapturedAmount")]
        public decimal TotalCapturedAmount { get; set; }

        [JsonPropertyName("totalRefundedAmount")]
        public decimal TotalRefundedAmount { get; set; }
    }
}