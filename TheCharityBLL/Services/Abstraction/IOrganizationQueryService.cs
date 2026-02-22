
using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.OrganizationDTOs;
using TheCharityDAL.Enums;

namespace TheCharityBLL.Services.Abstraction
{
    public interface IOrganizationQueryService
    {
        Task<ServiceResponce<IEnumerable<OrganizationResponseDto>>> SearchOrganizations(string searchTerm);
        Task<ServiceResponce<IEnumerable<OrganizationResponseDto>>> GetDeletedOrganizations();

        // Dashboard & Advanced Queries
        Task<ServiceResponce<IEnumerable<OrganizationResponseDto>>> GetByCampaignCount(int minCampaigns);
        Task<ServiceResponce<IEnumerable<OrganizationResponseDto>>> GetRecentlyRegisteredOrganizations(int days);
        Task<ServiceResponce<IEnumerable<OrganizationResponseDto>>> GetWithoutCampaigns();
        Task<ServiceResponce<IEnumerable<OrganizationResponseDto>>> GetWithoutPaymentInfo();
        Task<ServiceResponce<IEnumerable<OrganizationResponseDto>>> GetWithActiveCampaigns();
        Task<ServiceResponce<IEnumerable<OrganizationResponseDto>>> GetWithCompletedCampaigns();
        Task<ServiceResponce<IEnumerable<OrganizationResponseDto>>> GetByContactType(ContactType type);


        //Statistics
        Task<ServiceResponce<int>> GetTotalOrganizationsCount();
        Task<ServiceResponce<int>> GetActiveOrganizationsCount();
    }
}
