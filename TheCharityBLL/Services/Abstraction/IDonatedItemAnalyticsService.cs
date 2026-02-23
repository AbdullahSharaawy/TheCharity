using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.DonatedItemDTOs;
using TheCharityDAL.Enums;

namespace TheCharityBLL.Services.Abstraction
{
    public interface IDonatedItemAnalyticsService
    {
        Task<ServiceResponse<int>> GetTotalDonatedItemsCount();
        Task<ServiceResponse<int>> GetAvailableDonatedItemsCount();
        Task<ServiceResponse<int>> GetDonatedItemsCountByOrganization(int organizationId);
        //Task<ServiceResponce<int>> GetDonatedItemsCountByDonor(string donorId);
        Task<ServiceResponse<int>> GetDonatedItemsCountByCategory(ItemCategory category);
        Task<ServiceResponse<int>> GetOrganizationAvailableItemsCount(int organizationId);
        Task<ServiceResponse<int>> GetDonorTotalDonatedItemsCount(string donorId);
        Task<ServiceResponse<ItemCategory>> GetDonorMostCommonCategory(string donorId);

        Task<ServiceResponse<Dictionary<string, int>>> GetActivityByDonor(int days);

        Task<ServiceResponse<Dictionary<ItemCategory, int>>> GetDonatedItemsCountToAllCategories();
        Task<ServiceResponse<Dictionary<string, int>>> GetTopDonors(int limit);
        Task<ServiceResponse<Dictionary<ItemCategory, decimal>>> GetCategoryDistributionPercentage();
        Task<ServiceResponse<Dictionary<int, int>>> GetTopOrganizationsByDonations(int limit);
        Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetMostRecentDonatedItems(int limit);
        Task<ServiceResponse<Dictionary<DateTime, int>>> GetDonatedItemsTrend(int days);
        Task<ServiceResponse<Dictionary<ItemCategory, int>>> GetOrganizationInventoryByCategory(int organizationId);

    }
}
