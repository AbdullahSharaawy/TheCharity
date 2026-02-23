using TheCharityDAL.Entities;
using TheCharityDAL.Enums;

namespace TheCharityDAL.Repositories.Abstraction
{
    public interface IDonatedItemsRepository
    {
        // ===== DonatedItem CRUD Operations =====
        Task<IEnumerable<DonatedItem>> GetAllDonatedItemsAsync(bool includeDeleted = false);
        Task<DonatedItem?> GetDonatedItemByIdAsync(int id);
        //Task<DonatedItem?> GetDonatedItemByIdWithDetailsAsync(int id);
        Task<DonatedItem> AddDonatedItemAsync(DonatedItem donatedItem);
        Task<DonatedItem> UpdateDonatedItemAsync(DonatedItem donatedItem);
        Task DeleteDonatedItemAsync(int id);
        Task RestoreDonatedItemAsync(int id);

        // ===== DonatedItem Filtering & Search =====
        Task<IEnumerable<DonatedItem>> GetDonatedItemsByOrganizationAsync(int organizationId);
        Task<IEnumerable<DonatedItem>> GetDonatedItemsByDonorAsync(string donorId);
        Task<IEnumerable<DonatedItem>> GetDonatedItemsByCategoryAsync(ItemCategory category);
        Task<IEnumerable<DonatedItem>> GetAvailableDonatedItemsAsync();
        Task<IEnumerable<DonatedItem>> GetUnavailableDonatedItemsAsync();
        Task<IEnumerable<DonatedItem>> SearchDonatedItemsAsync(string searchTerm);
        Task<IEnumerable<DonatedItem>> GetDeletedDonatedItemsAsync();

        // ===== Item Images Management =====
        Task<IEnumerable<ItemImage>> GetItemImagesAsync(int donatedItemId);
        Task<ItemImage?> GetItemImageByIdAsync(int imageId);
        Task<ItemImage> AddItemImageAsync(ItemImage itemImage);
        Task<ItemImage> UpdateItemImageAsync(ItemImage itemImage);
        Task DeleteItemImageAsync(int imageId);
        Task<int> GetItemImageCountAsync(int donatedItemId);
        Task<ItemImage?> GetPrimaryItemImageAsync(int donatedItemId);

        // ===== Attachments Management =====
        Task<IEnumerable<Attachment>> GetItemAttachmentsAsync(int donatedItemId);
        Task<IEnumerable<Attachment>> GetRecipientAttachmentsAsync(int donatedItemId);
        Task<IEnumerable<Attachment>> GetAllAttachmentsAsync(int donatedItemId);
        Task<Attachment?> GetAttachmentByIdAsync(int attachmentId);
        Task<Attachment> AddAttachmentAsync(Attachment attachment);
        Task<Attachment> UpdateAttachmentAsync(Attachment attachment);
        Task DeleteAttachmentAsync(int attachmentId);

        // ===== DonatedItem Status Management =====
        Task<DonatedItem?> UpdateItemAvailabilityAsync(int itemId, bool isAvailable);
        Task<DonatedItem?> MarkItemAsAvailableAsync(int itemId);
        Task<DonatedItem?> MarkItemAsUnavailableAsync(int itemId);
        Task<DonatedItem?> UpdateItemCategoryAsync(int itemId, ItemCategory category);

        // ===== Statistics & Analytics =====
        Task<int> GetTotalDonatedItemsCountAsync();
        Task<int> GetAvailableDonatedItemsCountAsync();
        Task<int> GetDonatedItemsCountByOrganizationAsync(int organizationId);
        Task<int> GetDonatedItemsCountByDonorAsync(string donorId);
        Task<int> GetDonatedItemsCountByCategoryAsync(ItemCategory category);
        Task<Dictionary<ItemCategory, int>> GetDonatedItemsCountToAllCategoriesAsync();
        Task<Dictionary<string, int>> GetTopDonorsAsync(int limit = 10);
        Task<Dictionary<int, int>> GetTopOrganizationsByDonationsAsync(int limit = 10);

        // ===== Advanced Queries =====
        Task<IEnumerable<DonatedItem>> GetRecentDonatedItemsAsync(int days = 30);
        Task<IEnumerable<DonatedItem>> GetDonatedItemsWithoutImagesAsync();
        Task<IEnumerable<DonatedItem>> GetDonatedItemsWithAttachmentsAsync();
        Task<IEnumerable<DonatedItem>> GetDonatedItemsByDateRangeAsync(DateTime startDate, DateTime endDate);

        // ===== Bulk Operations =====
        Task<int> BulkUpdateItemCategoriesAsync(ItemCategory oldCategory, ItemCategory newCategory);
        Task<int> BulkMarkItemsAsUnavailableAsync(int organizationId);
        Task<int> DeleteOldDonatedItemsAsync(int daysOld = 365);

        // ===== Validation & Checks =====
        Task<bool> DonatedItemExistsAsync(int id);
        Task<bool> IsDonatedItemAvailableAsync(int id);
        Task<bool> HasDonatedItemImagesAsync(int id);
        Task<bool> HasDonatedItemAttachmentsAsync(int id);
        Task<bool> IsDonorAsync(string donorId);

        // ===== Eager Loading =====
        Task<DonatedItem?> GetDonatedItemWithImagesAsync(int id);
        Task<DonatedItem?> GetDonatedItemWithAttachmentsAsync(int id);
        Task<DonatedItem?> GetDonatedItemWithDonorAndOrganizationAsync(int id);

        // ===== Dashboard & Reporting =====
        Task<IEnumerable<DonatedItem>> GetMostRecentDonatedItemsAsync(int limit = 10);
        Task<Dictionary<DateTime, int>> GetDonatedItemsTrendAsync(int days = 30);

        // ===== Donor Management =====
        Task<IEnumerable<DonatedItem>> GetDonorDonatedItemsHistoryAsync(string donorId);
        Task<int> GetDonorTotalDonatedItemsCountAsync(string donorId);
        Task<ItemCategory> GetDonorMostCommonCategoryAsync(string donorId);

        // ===== Organization Management =====
        Task<IEnumerable<DonatedItem>> GetOrganizationInventoryAsync(int organizationId);
        Task<int> GetOrganizationAvailableItemsCountAsync(int organizationId);
        Task<Dictionary<ItemCategory, int>> GetOrganizationInventoryByCategoryAsync(int organizationId);

        // ===== File Management =====
        Task<long> GetTotalFileSizeAsync(int donatedItemId);
        Task<long> GetTotalStorageUsedAsync();
        Task<IEnumerable<Attachment>> GetLargeAttachmentsAsync(long sizeThreshold = 10485760); // 10MB default

        // ===== Category Management =====
        Task<IEnumerable<DonatedItem>> GetItemsByMultipleCategoriesAsync(IEnumerable<ItemCategory> categories);
        Task<Dictionary<ItemCategory, decimal>> GetCategoryDistributionPercentageAsync();
        Task<IEnumerable<ItemCategory>> GetMostPopularCategoriesAsync(int limit = 2);

        // ===== Search & Filter Combinations =====
        Task<IEnumerable<DonatedItem>> SearchAvailableItemsByCategoryAsync(string searchTerm, ItemCategory? category = null);
        Task<IEnumerable<DonatedItem>> GetItemsByOrganizationAndCategoryAsync(int organizationId, ItemCategory category);
        Task<IEnumerable<DonatedItem>> GetItemsByDonorAndDateRangeAsync(string donorId, DateTime startDate, DateTime endDate);

        // ===== Transfer Operations =====
        Task<DonatedItem?> TransferItemToOrganizationAsync(int itemId, int newOrganizationId);
        Task<DonatedItem?> UpdateItemDonorAsync(int itemId, string newDonorId);

        // ===== Audit & History =====
        Task<IEnumerable<DonatedItem>> GetRecentlyUpdatedItemsAsync(int hours = 24);
        Task<Dictionary<string, int>> GetActivityByDonorAsync(int days = 30);
    }
}
