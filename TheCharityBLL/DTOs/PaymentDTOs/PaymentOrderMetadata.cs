using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TheCharityBLL.DTOs.PaymentDTOs
{
    public class PaymentOrderMetadata
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; } = null!;

        [JsonPropertyName("campaign_id")]
        public int CampaignId { get; set; }
    }
}
