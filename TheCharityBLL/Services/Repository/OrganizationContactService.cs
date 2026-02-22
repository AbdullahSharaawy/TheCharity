
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
        public async Task<ServiceResponce<int>> AddContactMethod(CreateOrganizationContactMethodDto contactMethod)
        {
            if(await _repository.ContactMethodExistsAsync(contactMethod.CompanyId, contactMethod.Type, contactMethod.Value))
            {
                return new ServiceResponce<int>
                {
                    Success = false,
                    Message = "Contact method already exists for this organization.",
                };
            }
            var organizationContact = _mapper.MapToOrganizationContactMethod(contactMethod);
            var createdContactMethod = await _repository.AddContactMethodAsync(organizationContact);
            return new ServiceResponce<int>
            {
                Success = true,
                Message = "Contact method added successfully.",
                Data = createdContactMethod.Id
            };
        }

        public async Task<ServiceResponce<bool>> DeleteContactMethod(int contactMethodId)
        {
            var delete = _repository.DeleteContactMethodAsync(contactMethodId);
            return new ServiceResponce<bool>
            {
                Success = true,
                Message = "Contact method deleted successfully.",
            };
        }

        public async Task<ServiceResponce<IEnumerable<OrganizationContactMethodResponseDto>>> GetAllContactMethodsByOrganizationId(int organizationId)
        {
            if(!await _repository.OrganizationExistsAsync(organizationId))
            {
                return new ServiceResponce<IEnumerable<OrganizationContactMethodResponseDto>>
                {
                    Success = false,
                    Message = $"Organization with ID {organizationId} not found.",
                };
            }
            var contactMethods =await _repository.GetOrganizationContactMethodsAsync(organizationId);
            var contactMethodDtos = _mapper.MapToOrganizationContactMethodResponseDtos(contactMethods.ToList());
            return new ServiceResponce<IEnumerable<OrganizationContactMethodResponseDto>>
            {
                Success = true,
                Message = "Contact methods retrieved successfully.",
                Data = contactMethodDtos
            };
        }

        public async Task<ServiceResponce<OrganizationContactMethodResponseDto>> GetContactMethodById(int contactMethodId)
        {
            var contactMethod =await _repository.GetContactMethodByIdAsync(contactMethodId);
            if (contactMethod == null)
            {
                return new ServiceResponce<OrganizationContactMethodResponseDto>
                {
                    Success = false,
                    Message = $"Contact method with ID {contactMethodId} not found.",
                };
            }
            var contactMethodDto = _mapper.MapToOrganizationContactMethodResponseDto(contactMethod);
            return new ServiceResponce<OrganizationContactMethodResponseDto>
            {
                Success = true,
                Message = "Contact method retrieved successfully.",
                Data = contactMethodDto
            };
        }

        public async Task<ServiceResponce<int>> GetContactMethodCountByType(int organizationId, ContactType type)
        {
            var count =await _repository.GetContactMethodCountByTypeAsync(organizationId, type);
            return new ServiceResponce<int>
            {
                Success = true,
                Message = "Contact method count retrieved successfully.",
                Data = count
            };
        }

        public async Task<ServiceResponce<IEnumerable<OrganizationContactMethodResponseDto>>> GetContactMethodsByType(int organizationId, ContactType type)
        {
            var contactMethods =await _repository.GetContactMethodsByTypeAsync(organizationId, type);
            var contactMethodDtos = _mapper.MapToOrganizationContactMethodResponseDtos(contactMethods.ToList());
            return new ServiceResponce<IEnumerable<OrganizationContactMethodResponseDto>>
            {
                Success = true,
                Message = "Contact methods retrieved successfully.",
                Data = contactMethodDtos
            };
        }

        public async Task<ServiceResponce<bool>> RestoreContactMethod(int contactMethodId)
        {
            var restore = _repository.RestoreContactMethodAsync(contactMethodId);
            return new ServiceResponce<bool>
            {
                Success = true,
                Message = "Contact method restored successfully.",
            };  
        }

        public async Task<ServiceResponce<bool>> UpdateContactMethod(int id,UpdateOrganizationContactMethodDto contactMethod)
        {
            var existcontactMethod =await _repository.GetContactMethodByIdAsync(id);
            if (existcontactMethod==null)
            {
                return new ServiceResponce<bool>
                {
                    Success = false,
                    Message = $"Contact method with ID {id} not found.",
                };
            }
            existcontactMethod.EditValue(contactMethod.Value);
            existcontactMethod.EditType(contactMethod.Type);
            var update =await _repository.UpdateContactMethodAsync(existcontactMethod);
            return new ServiceResponce<bool>
            {
                Success = true,
                Message = "Contact method updated successfully.",
            };
        }
    }
}
