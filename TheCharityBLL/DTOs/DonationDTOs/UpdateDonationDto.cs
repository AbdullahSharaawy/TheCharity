using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCharityBLL.DTOs.CampaignDTOs;

namespace TheCharityBLL.DTOs.DonationDTOs
{
    public class UpdateDonationDto
    {

        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public int? Amount { get; set; }

        public int? CampaignId { get; set; }
    }
}
