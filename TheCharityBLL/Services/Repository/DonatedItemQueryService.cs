
using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.DonatedItemDTOs;
using TheCharityBLL.Mapper;
using TheCharityBLL.Services.Abstraction.DonatedItems;
using TheCharityDAL.Entities;
using TheCharityDAL.Enums;
using TheCharityDAL.Repositories.Abstraction;

namespace TheCharityBLL.Services.Repository
{
    public class DonatedItemQueryService : IDonatedItemQueryService
    {
        private readonly IDonatedItemsRepository _repository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly DontedItemMapper _mapper;
        public DonatedItemQueryService(IDonatedItemsRepository repository, IOrganizationRepository organizationRepository)
        {
            _repository = repository;
            _mapper = new DontedItemMapper();
            _organizationRepository = organizationRepository;
        }
        public async Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetAvailableDonatedItems()
        {
            var availableItems = await _repository.GetAvailableDonatedItemsAsync();
            var donatedItemDtos = _mapper.MapToDonatedItemResponseDtos(availableItems);
            return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
            {
                Success = true,
                Data = donatedItemDtos,
                Message = "Available donated items retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetDeletedDonatedItems()
        {
            var deletedItems = await _repository.GetDeletedDonatedItemsAsync();
            var donatedItemDtos = _mapper.MapToDonatedItemResponseDtos(deletedItems);
            return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
            {
                Success = true,
                Data = donatedItemDtos,
                Message = "Deleted donated items retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetDonatedItemsByCategory(ItemCategory category)
        {
            var itemsByCategory = await _repository.GetDonatedItemsByCategoryAsync(category);
            var donatedItemDtos = _mapper.MapToDonatedItemResponseDtos(itemsByCategory);
            return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
            {
                Success = true,
                Data = donatedItemDtos,
                Message = $"Donated items in category '{category}' retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetDonatedItemsByDateRange(DateTime startDate, DateTime endDate)
        {
            var itemsByDateRange = await _repository.GetDonatedItemsByDateRangeAsync(startDate, endDate);
            var donatedItemDtos = _mapper.MapToDonatedItemResponseDtos(itemsByDateRange);
            return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
            {
                Success = true,
                Data = donatedItemDtos,
                Message = $"Donated items registered between {startDate.ToShortDateString()} and {endDate.ToShortDateString()} retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetDonatedItemsByDonor(string donorId)
        {
            if (!await _repository.IsDonorAsync(donorId))
            {
                return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
                {
                    Success = false,
                    Data = null,
                    Message = $"Donor with ID {donorId} not found.",
                };
            }
            var itemsByDonor = await _repository.GetDonatedItemsByDonorAsync(donorId);
            var donatedItemDtos = _mapper.MapToDonatedItemResponseDtos(itemsByDonor);
            return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
            {
                Success = true,
                Data = donatedItemDtos,
                Message = $"Donated items by donor with ID {donorId} retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetDonatedItemsByOrganization(int organizationId)
        {
            if (!await _organizationRepository.OrganizationExistsAsync(organizationId))
            {
                return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
                {
                    Success = false,
                    Data = null,
                    Message = $"Organization with ID {organizationId} not found.",
                };
            }
            var donatedItemsByOrganization = await _repository.GetDonatedItemsByOrganizationAsync(organizationId);
            var donatedItemDtos = _mapper.MapToDonatedItemResponseDtos(donatedItemsByOrganization);
            return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
            {
                Success = true,
                Data = donatedItemDtos,
                Message = $"Donated items for organization with ID {organizationId} retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetDonatedItemsWithoutImages()
        {
            var donatedItemsWithoutImages = await _repository.GetDonatedItemsWithoutImagesAsync();
            var donatedItemDtos = _mapper.MapToDonatedItemResponseDtos(donatedItemsWithoutImages);
            return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
            {
                Success = true,
                Data = donatedItemDtos,
                Message = "Donated items without images retrieved successfully."
            };
        }


        public async Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetDonatedItemsByDonorAndDateRange(string donorId, DateTime startDate, DateTime endDate)
        {
            if (!await _repository.IsDonorAsync(donorId))
            {
                return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
                {
                    Success = false,
                    Data = null,
                    Message = $"Donor with ID {donorId} not found.",
                };
            }
            var itemsByDonorAndDateRange = await _repository.GetItemsByDonorAndDateRangeAsync(donorId, startDate, endDate);
            var donatedItemDtos = _mapper.MapToDonatedItemResponseDtos(itemsByDonorAndDateRange);
            return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
            {
                Success = true,
                Data = donatedItemDtos,
                Message = $"Donated items by donor with ID {donorId} registered between {startDate.ToShortDateString()} and {endDate.ToShortDateString()} retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetItemsByOrganizationAndCategory(int organizationId, ItemCategory category)
        {
            if (!await _organizationRepository.OrganizationExistsAsync(organizationId))
            {
                return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
                {
                    Success = false,
                    Data = null,
                    Message = $"Organization with ID {organizationId} not found.",
                };
            }
            var itemsByOrganizationAndCategory = await _repository.GetItemsByOrganizationAndCategoryAsync(organizationId, category);
            var donatedItemDtos = _mapper.MapToDonatedItemResponseDtos(itemsByOrganizationAndCategory);
            return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
            {
                Success = true,
                Data = donatedItemDtos,
                Message = $"Donated items for organization with ID {organizationId} in category '{category}' retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetOrganizationInventory(int organizationId)
        {
            if (!await _organizationRepository.OrganizationExistsAsync(organizationId))
            {
                return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
                {
                    Success = false,
                    Data = null,
                    Message = $"Organization with ID {organizationId} not found.",
                };
            }
            var organizationInventory = await _repository.GetOrganizationInventoryAsync(organizationId);
            var donatedItemDtos = _mapper.MapToDonatedItemResponseDtos(organizationInventory);
            return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
            {
                Success = true,
                Data = donatedItemDtos,
                Message = $"Inventory for organization with ID {organizationId} retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetRecentDonatedItems(int days)
        {
            var recentDonatedItems = await _repository.GetRecentDonatedItemsAsync(days);
            var donatedItemDtos = _mapper.MapToDonatedItemResponseDtos(recentDonatedItems);
            return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
            {
                Success = true,
                Data = donatedItemDtos,
                Message = $"Donated items registered in the last {days} days retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetRecentlyUpdatedItems(int hours)
        {
            var recentUpdatedItems = await _repository.GetRecentlyUpdatedItemsAsync(hours);
            var donatedItemDtos = _mapper.MapToDonatedItemResponseDtos(recentUpdatedItems);
            return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
            {
                Success = true,
                Data = donatedItemDtos,
                Message = $"Donated items updated in the last {hours} hours retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetUnavailableDonatedItems()
        {
            var unavailableItems = await _repository.GetUnavailableDonatedItemsAsync();
            var donatedItemDtos = _mapper.MapToDonatedItemResponseDtos(unavailableItems);
            return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
            {
                Success = true,
                Data = donatedItemDtos,
                Message = "Unavailable donated items retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> SearchAvailableItemsByCategory(string searchTerm, ItemCategory? category = null)
        {
            var searchResults = await _repository.SearchAvailableItemsByCategoryAsync(searchTerm, category);
            if (searchResults == null || !searchResults.Any())
            {
                return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
                {
                    Success = true,
                    Message = $"No available donated items found matching search term '{searchTerm}' and category '{category}'."
                };
            }
            var donatedItemDtos = _mapper.MapToDonatedItemResponseDtos(searchResults);
            return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
            {
                Success = true,
                Data = donatedItemDtos,
                Message = $"Available donated items matching search term '{searchTerm}' and category '{category}' retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> SearchDonatedItems(string searchTerm)
        {
            var searchResults = await _repository.SearchDonatedItemsAsync(searchTerm);
            if (searchResults == null || !searchResults.Any())
            {
                return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
                {
                    Success = true,
                    Message = $"No donated items found matching search term '{searchTerm}'."
                };
            }
            var donatedItemDtos = _mapper.MapToDonatedItemResponseDtos(searchResults);
            return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
            {
                Success = true,
                Data = donatedItemDtos,
                Message = $"Donated items matching search term '{searchTerm}' retrieved successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetDonatedItemsWithAttachments()
        {
            var itemsWithAttachments =await _repository.GetDonatedItemsWithAttachmentsAsync();
            var donatedItemDtos = _mapper.MapToDonatedItemResponseDtos(itemsWithAttachments);
            return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
            {
                Success = true,
                Data = donatedItemDtos,
                Message = "Donated items with attachments retrieved successfully."
            };
        }

        
    }
}
