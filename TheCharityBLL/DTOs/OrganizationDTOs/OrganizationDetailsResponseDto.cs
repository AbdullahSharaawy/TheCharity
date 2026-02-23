
using TheCharityBLL.DTOs.CampaignDTOs;
using TheCharityBLL.DTOs.OrganizationContactMethodDTOs;

namespace TheCharityBLL.DTOs.OrganizationDTOs
{
    public class OrganizationDetailsResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? PaymentId { get; set; }
        public List<OrganizationContactMethodResponseDto> ContactMethods { get; set; }
        //public List<SoloCampaignResponseDto> SoloCampaigns { get; set; }
        //public List<SharedCampaignResponseDto> SharedCampaigns { get; set; }
        public List<CampaignResponseDto> Campaigns { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
