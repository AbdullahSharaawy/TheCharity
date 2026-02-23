
using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.OrganizationDTOs;
using TheCharityDAL.Entities;
using TheCharityDAL.Enums;

namespace TheCharityBLL.Services.Abstraction
{
    public interface IOrganizationQueryService
    {
        Task<ServiceResponse<IEnumerable<OrganizationResponseDto>>> SearchOrganizations(string searchTerm);
        Task<ServiceResponse<IEnumerable<OrganizationResponseDto>>> GetDeletedOrganizations();

        // Dashboard & Advanced Queries
        Task<ServiceResponse<IEnumerable<OrganizationResponseDto>>> GetByCampaignCount(int minCampaigns);
        Task<ServiceResponse<IEnumerable<OrganizationResponseDto>>> GetRecentlyRegisteredOrganizations(int days);
        Task<ServiceResponse<IEnumerable<OrganizationResponseDropDownListDto>>> GetOrganizationsDropDownList();
        Task<ServiceResponse<IEnumerable<OrganizationResponseDto>>> GetWithoutCampaigns();////
        Task<ServiceResponse<IEnumerable<OrganizationResponseDto>>> GetWithoutPaymentInfo();
        Task<ServiceResponse<IEnumerable<OrganizationResponseDto>>> GetWithActiveCampaigns();
        Task<ServiceResponse<IEnumerable<OrganizationResponseDto>>> GetWithCompletedCampaigns();
        Task<ServiceResponse<IEnumerable<OrganizationResponseDto>>> GetByContactType(ContactType type);


        //Statistics
        Task<ServiceResponse<int>> GetTotalOrganizationsCount();
        Task<ServiceResponse<int>> GetActiveOrganizationsCount();
    }
}
