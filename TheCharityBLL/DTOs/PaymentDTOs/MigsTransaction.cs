using System;
using System.Text.Json.Serialization;

namespace TheCharityBLL.DTOs.PaymentDTOs
{
    public class MigsTransaction
    {
        [JsonPropertyName("acquirer")]
        public Acquirer? Acquirer { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("authenticationStatus")]
        public string? AuthenticationStatus { get; set; }

        [JsonPropertyName("authorizationCode")]
        public string? AuthorizationCode { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("receipt")]
        public string? Receipt { get; set; }

        [JsonPropertyName("reference")]
        public string? Reference { get; set; }

        [JsonPropertyName("source")]
        public string? Source { get; set; }

        [JsonPropertyName("stan")]
        public string? Stan { get; set; }

        [JsonPropertyName("terminal")]
        public string? Terminal { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }
}