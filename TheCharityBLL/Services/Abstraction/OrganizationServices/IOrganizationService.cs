using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.OrganizationDTOs;
using TheCharityDAL.Entities;

namespace TheCharityBLL.Services.Abstraction.Organization
{
    public interface IOrganizationService
    {
        //CRUD Operations
        Task<ServiceResponse<IEnumerable<OrganizationResponseDto>>> GetAllOrganizations(bool includeDeleted = false);
        Task<ServiceResponse<IEnumerable<OrganizationResponseDto>>> GetAllOrganizationsByAddress(string address);

        Task<ServiceResponse<OrganizationResponseDto>> GetOrganizationById(int id);//for update
        Task<ServiceResponse<OrganizationDetailsResponseDto>> GetOrganizationByIdWithDetails(int id);//for details
        Task<ServiceResponse<OrganizationResponseDto>> GetOrganizationByName(string name);

        Task<ServiceResponse<int>> AddOrganization(CreateOrganizationDto createOrganizationDto);
        Task<ServiceResponse<bool>> UpdateOrganization(int id,UpdateOrganizationDto updateOrganizationDto);

        Task<ServiceResponse<bool>> DeleteOrganization(int id);
        Task<ServiceResponse<bool>> RestoreOrganization(int id);

        ////Validation
        //Task<ServiceResponce<bool>> OrganizationExists(int id);
        //Task<ServiceResponce<bool>> OrganizationNameExists(string name);


    }
}
