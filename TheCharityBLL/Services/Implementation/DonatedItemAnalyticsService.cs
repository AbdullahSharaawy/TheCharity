
using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.DonatedItemDTOs;
using TheCharityBLL.Mapper;
using TheCharityBLL.Services.Abstraction;
using TheCharityBLL.Services.Abstraction.DonatedItems;
using TheCharityDAL.Enums;
using TheCharityDAL.Repositories.Abstraction;

namespace TheCharityBLL.Services.Repository
{
    public class DonatedItemAnalyticsService : IDonatedItemAnalyticsService
    {
        private readonly IDonatedItemsRepository _repository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly DontedItemMapper _mapper;
        public DonatedItemAnalyticsService(IDonatedItemsRepository repository, IOrganizationRepository organizationRepository)
        {
            _repository = repository;
            _mapper = new DontedItemMapper();
            _organizationRepository = organizationRepository;
        }
        public async Task<ServiceResponse<Dictionary<string, int>>> GetActivityByDonor(int days)
        {
            var activityData = await _repository.GetActivityByDonorAsync(days);
            return new ServiceResponse<Dictionary<string, int>>
            {
                Success = true,
                Data = activityData,
                Message = $"Donor activity for the past {days} days retrieved successfully."
            };
        }

        public async Task<ServiceResponse<int>> GetAvailableDonatedItemsCount()
        {
            var count =await _repository.GetAvailableDonatedItemsCountAsync();
            return new ServiceResponse<int>
            {
                Success = true,
                Data = count,
                Message = "Available donated items count retrieved successfully."
            };
        }

        public Task<ServiceResponse<Dictionary<ItemCategory, decimal>>> GetCategoryDistributionPercentage()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<int>> GetDonatedItemsCountByCategory(ItemCategory category)
        {
            var count =await _repository.GetDonatedItemsCountByCategoryAsync(category);
            return new ServiceResponse<int>
            {
                Success = true,
                Data = count,
                Message = $"Donated items count for category {category} retrieved successfully."
            };
        }

        public async Task<ServiceResponse<int>> GetDonatedItemsCountByOrganization(int organizationId)
        {
            if(!await _organizationRepository.OrganizationExistsAsync(organizationId))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = $"Organization with ID {organizationId} does not exist."
                };
            }
            var count =await _repository.GetDonatedItemsCountByOrganizationAsync(organizationId);
            return new ServiceResponse<int>
            {
                Success = true,
                Data = count,
                Message = $"Donated items count for organization ID {organizationId} retrieved successfully."
            };
        }

        public async Task<ServiceResponse<Dictionary<ItemCategory, int>>> GetDonatedItemsCountToAllCategories()
        {
            var categoryCounts =await _repository.GetDonatedItemsCountToAllCategoriesAsync();
            return new ServiceResponse<Dictionary<ItemCategory, int>>
            {
                Success = true,
                Data = categoryCounts,
                Message = "Donated items count for all categories retrieved successfully."
            };
        }

        public async Task<ServiceResponse<Dictionary<DateTime, int>>> GetDonatedItemsTrend(int days)
        {
            var trendData =await _repository.GetDonatedItemsTrendAsync(days);
            return new ServiceResponse<Dictionary<DateTime, int>>
            {
                Success = true,
                Data = trendData,
                Message = $"Donated items trend for the past {days} days retrieved successfully."
            };
        }

        public async Task<ServiceResponse<ItemCategory>> GetDonorMostCommonCategory(string donorId)
        {
            if(await _repository.IsDonorAsync(donorId))
            {
                return new ServiceResponse<ItemCategory>
                {
                    Success = false,
                    Message = $"Donor with ID {donorId} does not exist."
                };
            }
            var category =await _repository.GetDonorMostCommonCategoryAsync(donorId);
            return new ServiceResponse<ItemCategory>
            {
                Success = true,
                Data = category,
                Message = $"Most common category for donor ID {donorId} retrieved successfully."
            };
        }

        public async Task<ServiceResponse<int>> GetDonorTotalDonatedItemsCount(string donorId)
        {
            if(!await _repository.IsDonorAsync(donorId))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = $"Donor with ID {donorId} does not exist."
                };
            }
            var count =await _repository.GetDonorTotalDonatedItemsCountAsync(donorId);
            return new ServiceResponse<int>
            {
                Success = true,
                Data = count,
                Message = $"Total donated items count for donor ID {donorId} retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetMostRecentDonatedItems(int limit)
        {
            var recentItems =await _repository.GetMostRecentDonatedItemsAsync(limit);
            var recentItemsDto =_mapper.MapToDonatedItemResponseDtos(recentItems);
            return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
            {
                Success = true,
                Data = recentItemsDto,
                Message = $"Most recent {limit} donated items retrieved successfully."
            };
        }

        public async Task<ServiceResponse<int>> GetOrganizationAvailableItemsCount(int organizationId)
        {
            if(!await _organizationRepository.OrganizationExistsAsync(organizationId))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = $"Organization with ID {organizationId} does not exist."
                };
            }
            var count =await _repository.GetOrganizationAvailableItemsCountAsync(organizationId);
            return new ServiceResponse<int>
            {
                Success = true,
                Data = count,
                Message = $"Available items count for organization ID {organizationId} retrieved successfully."
            };
        }

        public async Task<ServiceResponse<Dictionary<ItemCategory, int>>> GetOrganizationInventoryByCategory(int organizationId)
        {
            var inventoryData =await _repository.GetOrganizationInventoryByCategoryAsync(organizationId);
            return new ServiceResponse<Dictionary<ItemCategory, int>>
            {
                Success = true,
                Data = inventoryData,
                Message = $"Organization inventory by category for organization ID {organizationId} retrieved successfully."
            };
        }

        public async Task<ServiceResponse<Dictionary<string, int>>> GetTopDonors(int limit)
        {
            var topDonors =await _repository.GetTopDonorsAsync(limit);
            return new ServiceResponse<Dictionary<string, int>>
            {
                Success = true,
                Data = topDonors,
                Message = $"Top {limit} donors retrieved successfully."
            };
        }

        public async Task<ServiceResponse<Dictionary<int, int>>> GetTopOrganizationsByDonations(int limit)
        {
            var topOrganizations =await _repository.GetTopOrganizationsByDonationsAsync(limit);
            return new ServiceResponse<Dictionary<int, int>>
            {
                Success = true,
                Data = topOrganizations,
                Message = $"Top {limit} organizations by donations retrieved successfully."
            };
        }

        public async Task<ServiceResponse<int>> GetTotalDonatedItemsCount()
        {
            var count =await _repository.GetTotalDonatedItemsCountAsync();
            return new ServiceResponse<int> {
                Success = true,
                Data = count,
                Message = "Total donated items count retrieved successfully."
            };              
        }
    }
}
