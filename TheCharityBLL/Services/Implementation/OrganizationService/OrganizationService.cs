using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.OrganizationDTOs;
using TheCharityBLL.Mapper;
using TheCharityBLL.Services.Abstraction.Organization;
using TheCharityDAL.Repositories.Abstraction;

namespace TheCharityBLL.Services.Implementation.OrganizationService
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _repository;
        private readonly OrganizationMaper _mapper;
        public OrganizationService(IOrganizationRepository repository)
        {
            _repository = repository;
            _mapper = new OrganizationMaper();
        }
        //we must use global exception handling 
        public async Task<ServiceResponse<int>> AddOrganization(CreateOrganizationDto createOrganizationDto)
        {
            //validation

            //
            if (await _repository.OrganizationNameExistsAsync(createOrganizationDto.Name))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Organization name already exists.",
                };
            }
            var organization = _mapper.MapToOrganization(createOrganizationDto);
            var createorganization = await _repository.AddOrganizationAsync(organization);
            //organization.RegistrationDate = DateTime.UtcNow;
            return new ServiceResponse<int>
            {
                Success = true,
                Data = createorganization.Id,
                Message = "Organization created successfully."
            };
        }

        public async Task<ServiceResponse<bool>> DeleteOrganization(int id)
        {
            //validation

            //
            if (!await _repository.OrganizationExistsAsync(id))
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Organization with ID {id} not found.",
                };
            }
            await _repository.DeleteOrganizationAsync(id);
            return new ServiceResponse<bool>
            {
                Success = true,
                Data = true,
                Message = "Organization deleted successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<OrganizationResponseDto>>> GetAllOrganizations(bool includeDeleted = false)
        {
            var organizations = await _repository.GetAllOrganizationsAsync(includeDeleted);
            var organizationDtos = _mapper.MapToOrganizationResponseDtos(organizations);
            return new ServiceResponse<IEnumerable<OrganizationResponseDto>>
            {
                Success = true,
                Data = organizationDtos,
                Message = "Organizations retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<OrganizationResponseDto>>> GetAllOrganizationsByAddress(string address)
        {
            var organizations = await _repository.GetOrganizationsByAddressAsync(address);
            var organizationDtos = _mapper.MapToOrganizationResponseDtos(organizations);
            return new ServiceResponse<IEnumerable<OrganizationResponseDto>>
            {
                Success = true,
                Data = organizationDtos,
                Message = "Organizations retrieved successfully."
            };
        }

        public async Task<ServiceResponse<OrganizationResponseDto>> GetOrganizationById(int id)
        {
            //payment info
            var organization = await _repository.GetOrganizationByIdAsync(id);
            if (organization == null)
            {
                return new ServiceResponse<OrganizationResponseDto>
                {
                    Success = false,
                    Message = $"Organization with ID {id} not found.",
                };
            }
            var organizationDto = _mapper.MapToOrganizationResponseDto(organization);
            return new ServiceResponse<OrganizationResponseDto>
            {
                Success = true,
                Data = organizationDto,
                Message = "Organization retrieved successfully."
            };
        }

        public async Task<ServiceResponse<OrganizationDetailsResponseDto>> GetOrganizationByIdWithDetails(int id)
        {
            var organization = await _repository.GetOrganizationWithDetailsAsync(id);
            if (organization == null)
            {
                return new ServiceResponse<OrganizationDetailsResponseDto>
                {
                    Success = false,
                    Message = $"Organization with ID {id} not found.",
                };
            }
            var organizationDto = _mapper.MapToOrganizationDetailsResponseDto(organization);
            return new ServiceResponse<OrganizationDetailsResponseDto>
            {
                Success = true,
                Data = organizationDto,
                Message = "Organization retrieved successfully."
            };
        }

        public async Task<ServiceResponse<OrganizationResponseDto>> GetOrganizationByName(string name)
        {
            var organization = await _repository.GetOrganizationByNameAsync(name);
            if (organization == null)
            {
                return new ServiceResponse<OrganizationResponseDto>
                {
                    Success = false,
                    Message = $"Organization with name {name} not found.",
                };
            }
            var organizationDto = _mapper.MapToOrganizationResponseDto(organization);
            return new ServiceResponse<OrganizationResponseDto>
            {
                Success = true,
                Data = organizationDto,
                Message = "Organization retrieved successfully."
            };
        }

        public async Task<ServiceResponse<bool>> RestoreOrganization(int id)
        {
            //validation

            //
            if (!await _repository.OrganizationExistsAsync(id))
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Organization with ID {id} not found.",
                };
            }
            await _repository.RestoreOrganizationAsync(id);
            return new ServiceResponse<bool>
            {
                Success = true,
                Message = "Organization restored successfully"
            };
        }

        public async Task<ServiceResponse<bool>> UpdateOrganization(int id,UpdateOrganizationDto updateOrganizationDto)
        {
            //validation

            //

            var existingOrganization = await _repository.GetOrganizationByIdAsync(id);
            if (existingOrganization == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Organization with ID {id} not found.",
                };
            }
            if (existingOrganization.Name != updateOrganizationDto.Name)
            {
                if (await _repository.OrganizationNameExistsAsync(updateOrganizationDto.Name))
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Organization name already exists.",
                    };
                }
            }

            existingOrganization.EditName(updateOrganizationDto.Name);
            existingOrganization.EditAddress(updateOrganizationDto.Address);
            var updateOrganization = await _repository.UpdateOrganizationAsync(existingOrganization);
            return new ServiceResponse<bool>
            {
                Success = true,
                Message = "Organization updated successfully."
            };
        }
    }
}
