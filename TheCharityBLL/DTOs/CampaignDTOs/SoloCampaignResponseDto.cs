using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCharityBLL.DTOs.OrganizationDTOs;

namespace TheCharityBLL.DTOs.CampaignDTOs
{
    public class SoloCampaignResponseDto : CampaignResponseDto
    {
        public OrganizationResponseDto? Organization { get; set; }
    }
}
