using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.DonatedItemDTOs;
using TheCharityDAL.Entities;
using TheCharityDAL.Enums;

namespace TheCharityBLL.Services.Abstraction
{
    public interface IDonatedItemQueryService
    {
        //advanced query

        Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetDonatedItemsByOrganization(int organizationId);
        Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetDonatedItemsByDonor(string donorId);
        Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetDonatedItemsByCategory(ItemCategory category);
        Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetAvailableDonatedItems();
        Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetUnavailableDonatedItems();
        Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> SearchDonatedItems(string searchTerm);
        Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetDeletedDonatedItems();
        /// 

        Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetRecentDonatedItems(int days);
        Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetDonatedItemsWithoutImages();
        Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetDonatedItemsWithAttachments();
        Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetDonatedItemsByDateRange(DateTime startDate, DateTime endDate);

        //Task<ServiceResponse<DonatedItemDetailsResponseDto>> GetDonatedItemWithImages(int id);
        //Task<ServiceResponse<DonatedItemDetailsResponseDto>> GetDonatedItemWithAttachments(int id);

        Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetOrganizationInventory(int organizationId);


        Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> SearchAvailableItemsByCategory(string searchTerm, ItemCategory? category = null);
        Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetItemsByOrganizationAndCategory(int organizationId, ItemCategory category);
        Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetDonatedItemsByDonorAndDateRange(string donorId, DateTime startDate, DateTime endDate);

        Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetRecentlyUpdatedItems(int hours);

    }
}
