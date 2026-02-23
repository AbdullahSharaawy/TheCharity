
using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.ItemImageDTOs;
using TheCharityDAL.Entities;

namespace TheCharityBLL.Services.Abstraction
{
    public interface IDonatedItemImageService
    {
        Task<ServiceResponse<IEnumerable<ItemImageResponseDto>>> GetItemImages(int donatedItemId);
        Task<ServiceResponse<ItemImageResponseDto>> GetItemImageById(int imageId);
        Task<ServiceResponse<int>> AddItemImage(CreateItemImageDto itemImage);
        //Task<ServiceResponse<bool>> UpdateItemImage(UpdateItemImageDto itemImage);
        Task<ServiceResponse<bool>> DeleteItemImage(int imageId);
        Task<ServiceResponse<int>> GetItemImageCount(int donatedItemId);
        Task<ServiceResponse<ItemImageResponseDto>> GetPrimaryItemImage(int donatedItemId);
    }
}
