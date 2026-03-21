using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.OrganizationContactMethodDTOs;
using TheCharityDAL.Enums;

namespace TheCharityBLL.Services.Abstraction.Organization
{
    public interface IOrganizationContactService
    {
        Task<ServiceResponse<IEnumerable<OrganizationContactMethodResponseDto>>> GetAllContactMethodsByOrganizationId(int organizationId);
        Task<ServiceResponse<OrganizationContactMethodResponseDto>> GetContactMethodById(int contactMethodId);
        Task<ServiceResponse<IEnumerable<OrganizationContactMethodResponseDto>>> GetContactMethodsByType(int organizationId, ContactType type);

        Task<ServiceResponse<int>> AddContactMethod(CreateOrganizationContactMethodDto contactMethod);
        Task<ServiceResponse<bool>> UpdateContactMethod(UpdateOrganizationContactMethodDto contactMethod);

        Task<ServiceResponse<bool>> DeleteContactMethod(int contactMethodId);
        Task<ServiceResponse<bool>> RestoreContactMethod(int contactMethodId);

        Task<ServiceResponse<int>> GetContactMethodCountByType(int organizationId, ContactType type);
    }
}
