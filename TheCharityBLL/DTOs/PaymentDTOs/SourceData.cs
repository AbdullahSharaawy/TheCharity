using System.Text.Json.Serialization;

namespace TheCharityBLL.DTOs.PaymentDTOs
{
    public class SourceData
    {
        [JsonPropertyName("pan")]
        public string? Pan { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("tenure")]
        public object? Tenure { get; set; }

        [JsonPropertyName("sub_type")]
        public string? SubType { get; set; }
    }
}