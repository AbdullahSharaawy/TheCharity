using System;
using System.Text.Json.Serialization;

namespace TheCharityBLL.DTOs.PaymentDTOs
{
    public class Acquirer
    {
        [JsonPropertyName("batch")]
        public long Batch { get; set; }

        [JsonPropertyName("date")]
        public string? Date { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("merchantId")]
        public string? MerchantId { get; set; }

        [JsonPropertyName("settlementDate")]
        public string? SettlementDate { get; set; }

        [JsonPropertyName("timeZone")]
        public string? TimeZone { get; set; }

        [JsonPropertyName("transactionId")]
        public string? TransactionId { get; set; }
    }
}