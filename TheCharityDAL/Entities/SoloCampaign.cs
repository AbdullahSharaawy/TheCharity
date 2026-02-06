using System.ComponentModel.DataAnnotations.Schema;
using TheCharityDAL.Enums;

namespace TheCharityDAL.Entities
{
    public class SoloCampaign: Campaign
    {
        public int? OrganizationId { get; private set; }

        [ForeignKey(nameof(OrganizationId))]
        public Organization? Organization { get; private set; }
        public SoloCampaign(string? title, string? description, string? imgPath, int? target, int? achieved, CampaignStatus? status, CampaignType? type, int? organizationId) : base(title, description, imgPath, target, achieved, status, type)
        {
            this.OrganizationId = organizationId;
        }
    }
}
