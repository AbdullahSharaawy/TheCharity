using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TheCharityBLL.DTOs.PaymentDTOs
{
    public class PaymobCallbackRequest
    {
        [JsonPropertyName("id")]
        public long TransactionId { get; set; }

        [JsonPropertyName("order")]
        public long OrderId { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("pending")]
        public bool Pending { get; set; }

        [JsonPropertyName("error_occured")]
        public bool ErrorOccurred { get; set; }

        [JsonPropertyName("has_parent_transaction")]
        public bool HasParentTransaction { get; set; }

        [JsonPropertyName("amount_cents")]
        public int AmountCents { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("owner")]
        public string Owner { get; set; }

        // Source data (card info)
        [JsonPropertyName("source_data.pan")]
        public string SourceDataPan { get; set; }

        [JsonPropertyName("source_data.type")]
        public string SourceDataType { get; set; }

        [JsonPropertyName("source_data.sub_type")]
        public string SourceDataSubType { get; set; }

        // HMAC sent by Paymob in the callback
        [JsonPropertyName("hmac")]
        public string Hmac { get; set; }
    }
}
