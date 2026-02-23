
using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.DonatedItemDTOs;
using TheCharityDAL.Enums;

namespace TheCharityBLL.Services.Abstraction
{
    public interface IDonatedItemService
    {
        Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetAllDonatedItems(bool includeDeleted );
        Task<ServiceResponse<DonatedItemResponseDto>> GetDonatedItemById(int id);
        Task<ServiceResponse<DonatedItemDetailsResponseDto>> GetDonatedItemByIdWithDetails(int id);

        Task<ServiceResponse<int>> AddDonatedItem(CreateDonatedItemDto donatedItem);
        Task<ServiceResponse<bool>> UpdateDonatedItem(UpdateDonatedItemDto donatedItem);

        Task<ServiceResponse<bool>> DeleteDonatedItem(int id);
        Task<ServiceResponse<bool>> RestoreDonatedItem(int id);

        Task<ServiceResponse<bool>> UpdateItemAvailability(int itemId, bool isAvailable);
        //Task<ServiceResponce<bool>> MarkItemAsAvailable(int itemId);
        //Task<ServiceResponce<bool>> MarkItemAsUnavailable(int itemId);
        Task<ServiceResponse<bool>> UpdateItemCategory(int itemId, ItemCategory category);

        Task<ServiceResponse<bool>> TransferItemToOrganization(int itemId, int newOrganizationId);
        Task<ServiceResponse<bool>> UpdateItemDonor(int itemId, string newDonorId);


        Task<ServiceResponse<int>> BulkUpdateItemCategories(ItemCategory oldCategory, ItemCategory newCategory);
        Task<ServiceResponse<int>> BulkMarkItemsAsUnavailable(int organizationId);
        Task<ServiceResponse<int>> DeleteOldDonatedItems(int daysOld );
  

    }
}
