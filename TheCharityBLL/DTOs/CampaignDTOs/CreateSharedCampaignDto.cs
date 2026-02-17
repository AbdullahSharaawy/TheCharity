using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCharityBLL.DTOs.CampaignDTOs
{
    public class CreateSharedCampaignDto : CreateCampaignDto
    {
        [MinLength(1, ErrorMessage = "At least one organization must be provided.")]
        public List<int> OrganizationIds { get; set; }
    }
}
