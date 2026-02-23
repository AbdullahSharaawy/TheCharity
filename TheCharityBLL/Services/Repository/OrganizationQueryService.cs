
using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.OrganizationDTOs;
using TheCharityBLL.Mapper;
using TheCharityBLL.Services.Abstraction;
using TheCharityDAL.Enums;
using TheCharityDAL.Repositories.Abstraction;

namespace TheCharityBLL.Services.Repository
{
    public class OrganizationQueryService : IOrganizationQueryService
    {
        private readonly IOrganizationRepository _repository;
        private readonly OrganizationMaper _mapper;
        public OrganizationQueryService(IOrganizationRepository repository)
        {
            _repository = repository;
            _mapper = new OrganizationMaper();
        }
        public async Task<ServiceResponse<int>> GetActiveOrganizationsCount()
        {
            var activeCount = await _repository.GetActiveOrganizationsCountAsync();
            return new ServiceResponse<int>
            {
                Success = true,
                Data = activeCount,
                Message = "Active organizations count retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<OrganizationResponseDto>>> GetByCampaignCount(int minCampaigns)
        {
            var organizations = await _repository.GetOrganizationsByCampaignCountAsync(minCampaigns);
            var organizationDtos = _mapper.MapToOrganizationResponseDtos(organizations);
            return new ServiceResponse<IEnumerable<OrganizationResponseDto>>
            {
                Success = true,
                Data = organizationDtos,
                Message = $"Organizations with at least {minCampaigns} campaigns retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<OrganizationResponseDto>>> GetByContactType(ContactType type)
        {
            var organizations = await _repository.GetOrganizationsByContactTypeAsync(type);
            var organizationDtos = _mapper.MapToOrganizationResponseDtos(organizations);
            return new ServiceResponse<IEnumerable<OrganizationResponseDto>>
            {
                Success = true,
                Data = organizationDtos,
                Message = $"Organizations with contact type {type} retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<OrganizationResponseDto>>> GetDeletedOrganizations()
        {
            var deletedOrganizations = await _repository.GetDeletedOrganizationsAsync();
            var organizationDtos = _mapper.MapToOrganizationResponseDtos(deletedOrganizations);
            return new ServiceResponse<IEnumerable<OrganizationResponseDto>>
            {
                Success = true,
                Data = organizationDtos,
                Message = "Deleted organizations retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<OrganizationResponseDto>>> GetRecentlyRegisteredOrganizations(int days)
        {
            var organizations = await _repository.GetRecentlyRegisteredOrganizationsAsync(days);
            var organizationDtos = _mapper.MapToOrganizationResponseDtos(organizations);
            return new ServiceResponse<IEnumerable<OrganizationResponseDto>>
            {
                Success = true,
                Data = organizationDtos,
                Message = $"Organizations registered in the last {days} days retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<OrganizationResponseDropDownListDto>>> GetOrganizationsDropDownList()
        {
            var organizations = await _repository.GetOrganizationsDropDownAsync();
            var organizationDtos = _mapper.MapToOrganizationResponseDropDownListDtos(organizations);
            return new ServiceResponse<IEnumerable<OrganizationResponseDropDownListDto>>
            {
                Success = true,
                Data = organizationDtos,
                Message = "Organizations for dropdown list retrieved successfully."
            };
        }

        public async Task<ServiceResponse<int>> GetTotalOrganizationsCount()
        {
            var totalCount = await _repository.GetTotalOrganizationsCountAsync();
            return new ServiceResponse<int>
            {
                Success = true,
                Data = totalCount,
                Message = "Total organizations count retrieved successfully."
            };

        }

        public async Task<ServiceResponse<IEnumerable<OrganizationResponseDto>>> GetWithActiveCampaigns()
        {
            var organizations = await _repository.GetOrganizationsWithActiveCampaignsAsync();
            var organizationDtos = _mapper.MapToOrganizationResponseDtos(organizations);
            return new ServiceResponse<IEnumerable<OrganizationResponseDto>>
            {
                Success = true,
                Data = organizationDtos,
                Message = "Organizations with active campaigns retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<OrganizationResponseDto>>> GetWithCompletedCampaigns()
        {
            var organizations = await _repository.GetOrganizationsWithCompletedCampaignsAsync();
            var organizationDtos = _mapper.MapToOrganizationResponseDtos(organizations);
            return new ServiceResponse<IEnumerable<OrganizationResponseDto>>
            {
                Success = true,
                Data = organizationDtos,
                Message = "Organizations with completed campaigns retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<OrganizationResponseDto>>> GetWithoutCampaigns()
        {
            var organizations = await _repository.GetOrganizationsWithoutCampaignsAsync();
            var organizationDtos = _mapper.MapToOrganizationResponseDtos(organizations);
            return new ServiceResponse<IEnumerable<OrganizationResponseDto>>
            {
                Success = true,
                Data = organizationDtos,
                Message = "Organizations without campaigns retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<OrganizationResponseDto>>> GetWithoutPaymentInfo()
        {
            var organizations = await _repository.GetOrganizationsWithoutPaymentInfoAsync();
            var organizationDtos = _mapper.MapToOrganizationResponseDtos(organizations);
            return new ServiceResponse<IEnumerable<OrganizationResponseDto>>
            {
                Success = true,
                Data = organizationDtos,
                Message = "Organizations without payment info retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<OrganizationResponseDto>>> SearchOrganizations(string searchTerm)
        {
            var organizations = await _repository.SearchOrganizationsAsync(searchTerm);
            if (organizations == null || !organizations.Any())
            {
                return new ServiceResponse<IEnumerable<OrganizationResponseDto>>
                {
                    Success = true,
                    Message = $"No organizations found matching search term '{searchTerm}'."
                };
            }
            var organizationDtos = _mapper.MapToOrganizationResponseDtos(organizations);
            return new ServiceResponse<IEnumerable<OrganizationResponseDto>>
            {
                Success = true,
                Data = organizationDtos,
                Message = $"Organizations matching search term '{searchTerm}' retrieved successfully."
            };
        }
    }
}
