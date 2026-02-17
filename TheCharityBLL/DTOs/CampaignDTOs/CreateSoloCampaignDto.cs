using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCharityBLL.DTOs.CampaignDTOs
{
    public class CreateSoloCampaignDto : CreateCampaignDto
    {
        [Required(ErrorMessage = "OrganizationId is required for a solo campaign.")]
        public int OrganizationId { get; set; }
    }
}
