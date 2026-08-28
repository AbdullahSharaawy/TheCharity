using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.OrganizationContactMethodDTOs;
using TheCharityBLL.DTOs.OrganizationDTOs;
using TheCharityBLL.DTOs.PaginationDTOs;
using TheCharityBLL.DTOs.UserDTOs;
using TheCharityDAL.Enums;

namespace TheCharityBLL.Services.Abstraction
{
    public interface IOrganizationService
    {
        // ===== Organization CRUD Operations =====
        Task<ServiceResponse<PagedResultDto<OrganizationResponseDto>>> GetAllOrganizationsAsync(PaginationParametersDto filterDto, bool includeDeleted = false);
        Task<ServiceResponse<OrganizationResponseDto>> GetOrganizationByIdAsync(int id);
        Task<ServiceResponse<OrganizationResponseDto>> CreateOrganizationAsync(CreateOrganizationDto organization);
        Task<ServiceResponse<OrganizationResponseDto>> UpdateOrganizationAsync(int id, UpdateOrganizationDto organization);
        Task<ServiceResponse<bool>> DeleteOrganizationAsync(int id);
        Task<ServiceResponse<bool>> RestoreOrganizationAsync(int id);

        // ===== Organization Filtering & Search =====
        Task<ServiceResponse<OrganizationResponseDto>> GetOrganizationByNameAsync(string name);
        Task<ServiceResponse<PagedResultDto<OrganizationResponseDto>>> SearchOrganizationsAsync(PaginationParametersDto filterDto, string searchTerm);
        Task<ServiceResponse<PagedResultDto<OrganizationResponseDto>>> GetDeletedOrganizationsAsync(PaginationParametersDto filterDto);
        Task<ServiceResponse<IEnumerable<OrganizationDropDownListDto>>> GetOrganizationsDropDownAsync();
        Task<ServiceResponse<PagedResultDto<OrganizationResponseDto>>> GetOrganizationsByAddressAsync(PaginationParametersDto filterDto, string address);

        // ===== Organization Statistics =====
        Task<ServiceResponse<int>> GetTotalOrganizationsCountAsync();
        Task<ServiceResponse<int>> GetActiveOrganizationsCountAsync();

        // ===== Organization Contact Methods =====
        Task<ServiceResponse<PagedResultDto<OrgContactMethodResponseDto>>> GetOrganizationContactMethodsAsync(PaginationParametersDto filterDto, int organizationId);
        Task<ServiceResponse<OrgContactMethodResponseDto>> GetContactMethodByIdAsync(int contactMethodId);
        Task<ServiceResponse<OrgContactMethodResponseDto>> CreateContactMethodAsync(CreateOrgContactMethodDto contactMethod);
        Task<ServiceResponse<OrgContactMethodResponseDto>> UpdateContactMethodAsync(int id, UpdateOrgContactMethodDto contactMethod);
        Task<ServiceResponse<bool>> DeleteContactMethodAsync(int contactMethodId);
        Task<ServiceResponse<bool>> RestoreContactMethodAsync(int contactMethodId);
        Task<ServiceResponse<PagedResultDto<OrgContactMethodResponseDto>>> GetContactMethodsByTypeAsync(PaginationParametersDto filterDto, int organizationId, ContactType type);

        // ===== Organization Performance =====
        Task<ServiceResponse<PagedResultDto<OrganizationResponseDto>>> GetOrganizationsByCampaignCountAsync(PaginationParametersDto filterDto, int minCampaigns = 1);

        // ===== Validation & Checks =====
        Task<ServiceResponse<bool>> OrganizationNameExistsAsync(string name);

        // ===== Eager Loading =====
        Task<ServiceResponse<OrganizationDetailsDto>> GetOrganizationDetailsAsync(int id);

        // ===== Dashboard & Reporting =====
        Task<ServiceResponse<PagedResultDto<OrganizationResponseDto>>> GetRecentlyRegisteredOrganizationsAsync(PaginationParametersDto filterDto, int days = 7);

        // ===== Advanced Queries =====
        Task<ServiceResponse<PagedResultDto<OrganizationResponseDto>>> GetOrganizationsWithoutCampaignsAsync(PaginationParametersDto filterDto);
        Task<ServiceResponse<PagedResultDto<OrganizationResponseDto>>> GetOrganizationsWithoutPaymentInfoAsync(PaginationParametersDto filterDto);
        Task<ServiceResponse<PagedResultDto<OrganizationResponseDto>>> GetOrganizationsWithActiveCampaignsAsync(PaginationParametersDto filterDto);
        Task<ServiceResponse<PagedResultDto<OrganizationResponseDto>>> GetOrganizationsWithCompletedCampaignsAsync(PaginationParametersDto filterDto);

        // ===== Contact Method Utilities =====
        Task<ServiceResponse<int>> GetContactMethodCountByTypeAsync(int organizationId, ContactType type);
        Task<ServiceResponse<PagedResultDto<OrganizationResponseDto>>> GetOrganizationsByContactTypeAsync(PaginationParametersDto filterDto, ContactType type);

        // ===== Payment Info Utilities =====
        Task<ServiceResponse<PagedResultDto<OrganizationResponseDto>>> GetOrganizationsWithValidPaymentInfoAsync(PaginationParametersDto filterDto);
        Task<ServiceResponse<Dictionary<int, DateTime>>> GetOrganizationLastPaymentUpdateAsync();

        // ===== Sub-Admin Management =====
        Task<ServiceResponse<OrganizationRoleResponseDto>> AddSubAdminAsync(int organizationId, string userId);
        Task<ServiceResponse<bool>> RemoveSubAdminAsync(int organizationId, string userId);
        Task<ServiceResponse<IEnumerable<UserResponseDTO>>> GetOrganizationSubAdminsAsync(int organizationId);
        Task<ServiceResponse<bool>> IsUserSubAdminAsync(int organizationId, string userId);

        // ===== Organization Admin Management =====
        Task<ServiceResponse<OrganizationResponseDto>> AssignOrganizationAdminAsync(int organizationId, string adminUserId);
        Task<ServiceResponse<OrganizationResponseDto>> RemoveOrganizationAdminAsync(int organizationId);
        Task<ServiceResponse<OrganizationResponseDto>> TransferOrganizationAdminAsync(int organizationId, string newAdminUserId);
        Task<ServiceResponse<UserResponseDTO?>> GetOrganizationAdminAsync(int organizationId);

    }
}
