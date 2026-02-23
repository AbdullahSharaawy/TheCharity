using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.DonatedItemDTOs;
using TheCharityDAL.Entities;
using TheCharityDAL.Enums;

namespace TheCharityBLL.Services.Abstraction
{
    public interface IDonatedItemQueryService
    {
        //advanced query
        
        Task<ServiceResponce<IEnumerable<DonatedItemResponseDto>>> GetDonatedItemsByOrganization(int organizationId);
        Task<ServiceResponce<IEnumerable<DonatedItemResponseDto>>> GetDonatedItemsByDonor(string donorId);
        Task<ServiceResponce<IEnumerable<DonatedItemResponseDto>>> GetDonatedItemsByCategory(ItemCategory category);
        Task<ServiceResponce<IEnumerable<DonatedItemResponseDto>>> GetAvailableDonatedItems();
        Task<ServiceResponce<IEnumerable<DonatedItemResponseDto>>> GetUnavailableDonatedItems();
        Task<ServiceResponce<IEnumerable<DonatedItemResponseDto>>> SearchDonatedItems(string searchTerm);
        Task<ServiceResponce<IEnumerable<DonatedItemResponseDto>>> GetDeletedDonatedItems();
        /// 
        //Task<ServiceResponce<IEnumerable<DonatedItemResponseDto>>> GetDonorDonatedItemsHistory(string donorId);


        Task<ServiceResponce<IEnumerable<DonatedItemResponseDto>>> GetRecentDonatedItems(int days);
        Task<ServiceResponce<IEnumerable<DonatedItemResponseDto>>> GetDonatedItemsWithoutImages();
        Task<ServiceResponce<IEnumerable<DonatedItemResponseDto>>> GetDonatedItemsWithAttachments();
        Task<ServiceResponce<IEnumerable<DonatedItemResponseDto>>> GetDonatedItemsByDateRange(DateTime startDate, DateTime endDate);

        Task<ServiceResponce<DonatedItemResponseDto>> GetDonatedItemWithImages(int id);
        Task<ServiceResponce<DonatedItemResponseDto>> GetDonatedItemWithAttachments(int id);
        Task<ServiceResponce<DonatedItemResponseDto>> GetDonatedItemWithDonorAndOrganization(int id);

        Task<ServiceResponce<IEnumerable<DonatedItemResponseDto>>> GetOrganizationInventory(int organizationId);
        

        Task<ServiceResponce<IEnumerable<DonatedItemResponseDto>>> SearchAvailableItemsByCategory(string searchTerm, ItemCategory? category = null);
        Task<ServiceResponce<IEnumerable<DonatedItemResponseDto>>> GetItemsByOrganizationAndCategory(int organizationId, ItemCategory category);
        Task<ServiceResponce<IEnumerable<DonatedItemResponseDto>>> GetItemsByDonorAndDateRange(string donorId, DateTime startDate, DateTime endDate);

        Task<ServiceResponce<IEnumerable<DonatedItemResponseDto>>> GetRecentlyUpdatedItems(int hours);

    }
}
