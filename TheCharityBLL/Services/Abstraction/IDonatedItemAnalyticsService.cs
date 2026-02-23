using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.DonatedItemDTOs;
using TheCharityDAL.Enums;

namespace TheCharityBLL.Services.Abstraction
{
    public interface IDonatedItemAnalyticsService
    {
        Task<ServiceResponce<int>> GetTotalDonatedItemsCount();
        Task<ServiceResponce<int>> GetAvailableDonatedItemsCount();
        Task<ServiceResponce<int>> GetDonatedItemsCountByOrganization(int organizationId);
        //Task<ServiceResponce<int>> GetDonatedItemsCountByDonor(string donorId);
        Task<ServiceResponce<int>> GetDonatedItemsCountByCategory(ItemCategory category);
        Task<ServiceResponce<int>> GetOrganizationAvailableItemsCount(int organizationId);
        Task<ServiceResponce<int>> GetDonorTotalDonatedItemsCount(string donorId);
        Task<ServiceResponce<ItemCategory>> GetDonorMostCommonCategory(string donorId);

        Task<ServiceResponce<Dictionary<string, int>>> GetActivityByDonor(int days);

        Task<ServiceResponce<Dictionary<ItemCategory, int>>> GetDonatedItemsCountToAllCategories();
        Task<ServiceResponce<Dictionary<string, int>>> GetTopDonors(int limit);
        Task<ServiceResponce<Dictionary<ItemCategory, decimal>>> GetCategoryDistributionPercentage();
        Task<ServiceResponce<Dictionary<int, int>>> GetTopOrganizationsByDonations(int limit);
        Task<ServiceResponce<IEnumerable<DonatedItemResponseDto>>> GetMostRecentDonatedItems(int limit);
        Task<ServiceResponce<Dictionary<DateTime, int>>> GetDonatedItemsTrend(int days);
        Task<ServiceResponce<Dictionary<ItemCategory, int>>> GetOrganizationInventoryByCategory(int organizationId);

    }
}
