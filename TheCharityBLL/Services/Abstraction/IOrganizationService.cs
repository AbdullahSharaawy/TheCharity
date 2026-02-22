
using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.OrganizationDTOs;
using TheCharityDAL.Entities;

namespace TheCharityBLL.Services.Abstraction
{
    public interface IOrganizationService
    {
        //CRUD Operations
        Task<ServiceResponce<IEnumerable<OrganizationResponseDto>>> GetAll(bool includeDeleted = false);
        Task<ServiceResponce<IEnumerable<OrganizationResponseDto>>> GetAllByAddress(string address);

        Task<ServiceResponce<OrganizationResponseDto>> GetById(int id);
        Task<ServiceResponce<OrganizationResponseDto>> GetByIdWithDetails(int id);
        Task<ServiceResponce<OrganizationResponseDto>> GetByName(string name);
        
        Task<ServiceResponce<int>> AddOrganization(CreateOrganizationDto createOrganizationDto);
        Task<ServiceResponce<bool>> UpdateOrganization(int id,UpdateOrganizationDto updateOrganizationDto); 
        
        Task<ServiceResponce<bool>> DeleteOrganization(int id);
        Task<ServiceResponce<bool>> RestoreOrganization(int id);

        ////Validation
        //Task<ServiceResponce<bool>> OrganizationExists(int id);
        //Task<ServiceResponce<bool>> OrganizationNameExists(string name);


    }
}
