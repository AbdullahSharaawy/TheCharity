
using TheCharityBLL.DTOs.OrganizationDTOs;
using TheCharityBLL.Services.Abstraction;
using TheCharityDAL.Repositories.Abstraction;

namespace TheCharityBLL.Services.Repository
{
    public class OraganizationSerice : IOrganizationService
    {
        private readonly IOrganizationRepository _repository;
        public OraganizationSerice(IOrganizationRepository repository)
        {
            _repository = repository;
        }
        public Task<bool> AddOrganization(CreateOrganizationDto createOrganizationDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrganization(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrganizationResponseDto>> GetAll(bool includeDeleted = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrganizationResponseDto>> GetAllByAddress(string address)
        {
            throw new NotImplementedException();
        }

        public Task<OrganizationResponseDto?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OrganizationResponseDto> GetByIdWithDetails(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OrganizationResponseDto> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> OrganizationExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> OrganizationNameExists(string name)
        {
            throw new NotImplementedException();
        }

        public Task RestoreOrganization(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOrganization(UpdateOrganizationDto updateOrganizationDto)
        {
            throw new NotImplementedException();
        }
    }
}
