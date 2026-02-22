
using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.OrganizationDTOs;
using TheCharityBLL.Mapper;
using TheCharityBLL.Services.Abstraction;
using TheCharityDAL.Repositories.Abstraction;

namespace TheCharityBLL.Services.Repository
{
    public class OraganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _repository;
        private readonly OrganizationMaper _mapper;
        public OraganizationService(IOrganizationRepository repository)
        {
            _repository = repository;
            _mapper = new OrganizationMaper();
        }
        //we must use global exception handling 
        public async Task<ServiceResponce<int>> AddOrganization(CreateOrganizationDto createOrganizationDto)
        {
            //validation

            //
            if (await _repository.OrganizationNameExistsAsync(createOrganizationDto.Name))
            {
                return new ServiceResponce<int>
                {
                    Success = false,
                    Message = "Organization name already exists.",
                };
            }
            var organization = _mapper.MapToOrganization(createOrganizationDto);
            var createorganization = await _repository.AddOrganizationAsync(organization);
            //organization.RegistrationDate = DateTime.UtcNow;
            return new ServiceResponce<int>
            {
                Success = true,
                Data = createorganization.Id,
                Message = "Organization created successfully."
            };
        }

        public async Task<ServiceResponce<bool>> DeleteOrganization(int id)
        {
            //validation

            //
            if (!await _repository.OrganizationExistsAsync(id))
            {
                return new ServiceResponce<bool>
                {
                    Success = false,
                    Message = $"Organization with ID {id} not found.",
                };
            }
            var delete = _repository.DeleteOrganizationAsync(id);
            return new ServiceResponce<bool>
            {
                Success = true,
                Data = true,
                Message = "Organization deleted successfully."
            };
        }

        public async Task<ServiceResponce<IEnumerable<OrganizationResponseDto>>> GetAll(bool includeDeleted = false)
        {
            var organizations = await _repository.GetAllOrganizationsAsync(includeDeleted);
            var organizationDtos = _mapper.MapToOrganizationResponseDtos(organizations);
            return new ServiceResponce<IEnumerable<OrganizationResponseDto>>
            {
                Success = true,
                Data = organizationDtos,
                Message = "Organizations retrieved successfully."
            };
        }

        public async Task<ServiceResponce<IEnumerable<OrganizationResponseDto>>> GetAllByAddress(string address)
        {
            var organizations = await _repository.GetOrganizationsByAddressAsync(address);
            var organizationDtos = _mapper.MapToOrganizationResponseDtos(organizations);
            return new ServiceResponce<IEnumerable<OrganizationResponseDto>>
            {
                Success = true,
                Data = organizationDtos,
                Message = "Organizations retrieved successfully."
            };
        }

        public async Task<ServiceResponce<OrganizationResponseDto>> GetById(int id)
        {
            var organization = await _repository.GetOrganizationByIdAsync(id);
            if(organization == null)
            {
                return new ServiceResponce<OrganizationResponseDto>
                {
                    Success = false,
                    Message = $"Organization with ID {id} not found.",
                };
            }
            var organizationDto = _mapper.MapToOrganizationResponseDto(organization);
            return new ServiceResponce<OrganizationResponseDto>
            {
                Success = true,
                Data = organizationDto,
                Message = "Organization retrieved successfully."
            };
        }

        public async Task<ServiceResponce<OrganizationResponseDto>> GetByIdWithDetails(int id)
        {
            var organization = await _repository.GetOrganizationWithDetailsAsync(id);
            if(organization == null)
            {
                return new ServiceResponce<OrganizationResponseDto>
                {
                    Success = false,
                    Message = $"Organization with ID {id} not found.",
                };
            }
            var organizationDto = _mapper.MapToOrganizationResponseDto(organization);
            return new ServiceResponce<OrganizationResponseDto>
            {
                Success = true,
                Data = organizationDto,
                Message = "Organization retrieved successfully."
            };
        }

        public async Task<ServiceResponce<OrganizationResponseDto>> GetByName(string name)
        {
            var organization = await _repository.GetOrganizationByNameAsync(name);
            if(organization == null)
            {
                return new ServiceResponce<OrganizationResponseDto>
                {
                    Success = false,
                    Message = $"Organization with name {name} not found.",
                };
            }
            var organizationDto = _mapper.MapToOrganizationResponseDto(organization);
            return new ServiceResponce<OrganizationResponseDto>
            {
                Success = true,
                Data = organizationDto,
                Message = "Organization retrieved successfully."
            };
        }

        public async Task<ServiceResponce<bool>> RestoreOrganization(int id)
        {
            //validation

            //
            if (!await _repository.OrganizationExistsAsync(id))
            {
                return new ServiceResponce<bool>
                {
                    Success = false,
                    Message = $"Organization with ID {id} not found.",
                };
            }
            var restore = _repository.RestoreOrganizationAsync(id);
            return new ServiceResponce<bool>
            {
                Success = true,
                Message = "Organization restored successfully"
            };
        }

        public async Task<ServiceResponce<bool>> UpdateOrganization(int id, UpdateOrganizationDto updateOrganizationDto)
        {
            //validation

            //
            var existingOrganization = await _repository.GetOrganizationByIdAsync(id);
            if (existingOrganization == null)
            {
                return new ServiceResponce<bool>
                {
                    Success = false,
                    Message = $"Organization with ID {id} not found.",
                };
            }
            existingOrganization.EditName(updateOrganizationDto.Name);
            existingOrganization.EditAddress(updateOrganizationDto.Address);
            var updateOrganization = await _repository.UpdateOrganizationAsync(existingOrganization);
            return new ServiceResponce<bool>
            {
                Success = true,
                Message = "Organization updated successfully."
            };
        }
    }
}
