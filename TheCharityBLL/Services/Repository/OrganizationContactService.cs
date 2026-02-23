
using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.OrganizationContactMethodDTOs;
using TheCharityBLL.Mapper;
using TheCharityBLL.Services.Abstraction;
using TheCharityDAL.Enums;
using TheCharityDAL.Repositories.Abstraction;

namespace TheCharityBLL.Services.Repository
{
    public class OrganizationContactService : IOrganizationContactService
    {
        private readonly IOrganizationRepository _repository;
        private readonly OrganizationContactMapper _mapper;
        public OrganizationContactService(IOrganizationRepository repository)
        {
            _repository = repository;
            _mapper = new OrganizationContactMapper();
        }
        public async Task<ServiceResponse<int>> AddContactMethod(CreateOrganizationContactMethodDto contactMethod)
        {
            if (await _repository.ContactMethodExistsAsync(contactMethod.CompanyId, contactMethod.Type, contactMethod.Value))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "Contact method already exists for this organization.",
                };
            }
            var organizationContact = _mapper.MapToOrganizationContactMethod(contactMethod);
            var createdContactMethod = await _repository.AddContactMethodAsync(organizationContact);
            return new ServiceResponse<int>
            {
                Success = true,
                Message = "Contact method added successfully.",
                Data = createdContactMethod.Id
            };
        }

        public async Task<ServiceResponse<bool>> DeleteContactMethod(int contactMethodId)
        {
            if (await _repository.GetContactMethodByIdAsync(contactMethodId)==null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Contact method with ID {contactMethodId} not found.",
                };
            }
            await _repository.DeleteContactMethodAsync(contactMethodId);
            return new ServiceResponse<bool>
            {
                Success = true,
                Message = "Contact method deleted successfully.",
            };
        }

        public async Task<ServiceResponse<IEnumerable<OrganizationContactMethodResponseDto>>> GetAllContactMethodsByOrganizationId(int organizationId)
        {
            if(!await _repository.OrganizationExistsAsync(organizationId))
            {
                return new ServiceResponse<IEnumerable<OrganizationContactMethodResponseDto>>
                {
                    Success = false,
                    Message = $"Organization with ID {organizationId} not found.",
                };
            }
            var contactMethods =await _repository.GetOrganizationContactMethodsAsync(organizationId);
            var contactMethodDtos = _mapper.MapToOrganizationContactMethodResponseDtos(contactMethods.ToList());
            return new ServiceResponse<IEnumerable<OrganizationContactMethodResponseDto>>
            {
                Success = true,
                Message = "Contact methods retrieved successfully.",
                Data = contactMethodDtos
            };
        }

        public async Task<ServiceResponse<OrganizationContactMethodResponseDto>> GetContactMethodById(int contactMethodId)
        {
            var contactMethod =await _repository.GetContactMethodByIdAsync(contactMethodId);
            if (contactMethod == null)
            {
                return new ServiceResponse<OrganizationContactMethodResponseDto>
                {
                    Success = false,
                    Message = $"Contact method with ID {contactMethodId} not found.",
                };
            }
            var contactMethodDto = _mapper.MapToOrganizationContactMethodResponseDto(contactMethod);
            return new ServiceResponse<OrganizationContactMethodResponseDto>
            {
                Success = true,
                Message = "Contact method retrieved successfully.",
                Data = contactMethodDto
            };
        }

        public async Task<ServiceResponse<int>> GetContactMethodCountByType(int organizationId, ContactType type)
        {
            var count =await _repository.GetContactMethodCountByTypeAsync(organizationId, type);
            return new ServiceResponse<int>
            {
                Success = true,
                Message = "Contact method count retrieved successfully.",
                Data = count
            };
        }

        public async Task<ServiceResponse<IEnumerable<OrganizationContactMethodResponseDto>>> GetContactMethodsByType(int organizationId, ContactType type)
        {
            var contactMethods =await _repository.GetContactMethodsByTypeAsync(organizationId, type);
            var contactMethodDtos = _mapper.MapToOrganizationContactMethodResponseDtos(contactMethods.ToList());
            return new ServiceResponse<IEnumerable<OrganizationContactMethodResponseDto>>
            {
                Success = true,
                Message = "Contact methods retrieved successfully.",
                Data = contactMethodDtos
            };
        }

        public async Task<ServiceResponse<bool>> RestoreContactMethod(int contactMethodId)
        {
            if(await _repository.GetContactMethodByIdAsync(contactMethodId)==null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Contact method with ID {contactMethodId} not found.",
                };
            }
            await _repository.RestoreContactMethodAsync(contactMethodId);
            return new ServiceResponse<bool>
            {
                Success = true,
                Message = "Contact method restored successfully.",
            };  
        }

        public async Task<ServiceResponse<bool>> UpdateContactMethod(UpdateOrganizationContactMethodDto contactMethod)
        {
            var existcontactMethod =await _repository.GetContactMethodByIdAsync(contactMethod.Id);
            if (existcontactMethod==null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Contact method with ID {contactMethod.Id} not found.",
                };
            }
            existcontactMethod.EditValue(contactMethod.Value);
            existcontactMethod.EditType(contactMethod.Type);
            var update =await _repository.UpdateContactMethodAsync(existcontactMethod);
            return new ServiceResponse<bool>
            {
                Success = true,
                Message = "Contact method updated successfully.",
            };
        }
    }
}
