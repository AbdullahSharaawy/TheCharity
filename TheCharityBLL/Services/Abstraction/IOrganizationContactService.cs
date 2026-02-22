

using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.OrganizationContactMethodDTOs;
using TheCharityDAL.Enums;

namespace TheCharityBLL.Services.Abstraction
{
    public interface IOrganizationContactService
    {
        Task<ServiceResponce<IEnumerable<OrganizationContactMethodResponseDto>>> GetAllContactMethodsByOrganizationId(int organizationId);
        Task<ServiceResponce<OrganizationContactMethodResponseDto>> GetContactMethodById(int contactMethodId);
        Task<ServiceResponce<IEnumerable<OrganizationContactMethodResponseDto>>> GetContactMethodsByType(int organizationId, ContactType type);

        Task<ServiceResponce<int>> AddContactMethod(CreateOrganizationContactMethodDto contactMethod);
        Task<ServiceResponce<bool>> UpdateContactMethod(int id,UpdateOrganizationContactMethodDto contactMethod);

        Task<ServiceResponce<bool>> DeleteContactMethod(int contactMethodId);
        Task<ServiceResponce<bool>> RestoreContactMethod(int contactMethodId);

        Task<ServiceResponce<int>> GetContactMethodCountByType(int organizationId, ContactType type);
    }
}
