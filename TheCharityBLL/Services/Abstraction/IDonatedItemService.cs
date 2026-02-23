
using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.DonatedItemDTOs;
using TheCharityDAL.Enums;

namespace TheCharityBLL.Services.Abstraction
{
    public interface IDonatedItemService
    {
        Task<ServiceResponce<IEnumerable<DonatedItemResponseDto>>> GetAllDonatedItems(bool includeDeleted = false);
        Task<ServiceResponce<DonatedItemResponseDto>> GetDonatedItemById(int id);

        Task<ServiceResponce<int>> AddDonatedItem(CreateDonatedItemDto donatedItem);
        Task<ServiceResponce<bool>> UpdateDonatedItem(int id,UpdateDonatedItemDto donatedItem);

        Task<ServiceResponce<bool>> DeleteDonatedItem(int id);
        Task<ServiceResponce<bool>> RestoreDonatedItem(int id);

        Task<ServiceResponce<bool>> UpdateItemAvailability(int itemId, bool isAvailable);
        //Task<ServiceResponce<bool>> MarkItemAsAvailable(int itemId);
        //Task<ServiceResponce<bool>> MarkItemAsUnavailable(int itemId);
        Task<ServiceResponce<bool>> UpdateItemCategory(int itemId, ItemCategory category);

        Task<ServiceResponce<bool>> TransferItemToOrganization(int itemId, int newOrganizationId);
        Task<ServiceResponce<bool>> UpdateItemDonor(int itemId, string newDonorId);


        //// ===== Validation & Checks =====
        //Task<ServiceResponce<bool> DonatedItemExists(int id);
        //Task<ServiceResponce<bool> IsDonatedItemAvailable(int id);
        //Task<ServiceResponce<bool> HasDonatedItemImages(int id);
        //Task<ServiceResponce<bool> HasDonatedItemAttachments(int id);
        //Task<ServiceResponce<bool> IsDonor(string donorId);  

    }
}
