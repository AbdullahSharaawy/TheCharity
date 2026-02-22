
using TheCharityBLL.DTOs.OrganizationDTOs;

namespace TheCharityBLL.Services.Abstraction
{
    public interface IOrganizationQueryService
    {
        Task<IEnumerable<OrganizationResponseDto>> SearchOrganizations(string searchTerm);
        Task<IEnumerable<OrganizationResponseDto>> GetDeletedOrganizations();

        // Dashboard & Advanced Queries
        Task<IEnumerable<OrganizationResponseDto>> GetByCampaignCount(int minCampaigns);
        Task<IEnumerable<OrganizationResponseDto>> GetRecentlyRegisteredOrganizations(int days);
        Task<IEnumerable<OrganizationResponseDto>> GetWithoutCampaigns();
        Task<IEnumerable<OrganizationResponseDto>> GetWithoutPaymentInfo();
        Task<IEnumerable<OrganizationResponseDto>> GetWithActiveCampaigns();
        Task<IEnumerable<OrganizationResponseDto>> GetWithCompletedCampaigns();

        //Statistics
        Task<int> GetTotalOrganizationsCount();
        Task<int> GetActiveOrganizationsCount();
    }
}
