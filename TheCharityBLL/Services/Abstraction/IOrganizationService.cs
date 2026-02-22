
using TheCharityBLL.DTOs.OrganizationDTOs;
using TheCharityDAL.Entities;

namespace TheCharityBLL.Services.Abstraction
{
    public interface IOrganizationService
    {
        //CRUD Operations
        Task<IEnumerable<OrganizationResponseDto>> GetAll(bool includeDeleted = false);
        Task<IEnumerable<OrganizationResponseDto>> GetAllByAddress(string address);

        Task<OrganizationResponseDto?> GetById(int id);
        Task<OrganizationResponseDto> GetByIdWithDetails(int id);
        Task<OrganizationResponseDto> GetByName(string name);
        
        Task<bool> AddOrganization(CreateOrganizationDto createOrganizationDto);
        Task<bool> UpdateOrganization(UpdateOrganizationDto updateOrganizationDto); 
        
        Task DeleteOrganization(int id);
        Task RestoreOrganization(int id);

        //Validation
        Task<bool> OrganizationExists(int id);
        Task<bool> OrganizationNameExists(string name);


    }
}
