using System.Text.Json.Serialization;

namespace TheCharityBLL.DTOs.PaymentDTOs
{
    public class Chargeback
    {
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }
    }
}