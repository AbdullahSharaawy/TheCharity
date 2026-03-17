
using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.ItemImageDTOs;
using TheCharityBLL.Helpers;
using TheCharityBLL.Mapper;
using TheCharityBLL.Services.Abstraction.DonatedItems;
using TheCharityDAL.Repositories.Abstraction;

namespace TheCharityBLL.Services.Repository
{
    public class DonatedItemImageService : IDonatedItemImageService
    {
        private readonly IDonatedItemsRepository _repository;
        private readonly ItemImageMapper _mapper;
        public DonatedItemImageService(IDonatedItemsRepository repository)
        {
            _repository = repository;
            _mapper = new ItemImageMapper();
        }
        public async Task<ServiceResponse<int>> AddItemImage(CreateItemImageDto itemImage)
        {
            if (!await _repository.DonatedItemExistsAsync(itemImage.DonatedItemId))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = $"Donated item with ID {itemImage.DonatedItemId} not found.",
                    Data = 0
                };
            }
            if (itemImage.ImageUrl != null && itemImage.ImageUrl.Length > 0)
            {
                var fileName = FileManager.UploadFile("DonatedItems/Images", itemImage.ImageUrl);
                if (!fileName.StartsWith("/"))
                {
                    return new ServiceResponse<int>
                    {
                        Success = false,
                        Message = $"Failed to upload image: {fileName}",
                    };
                }
                itemImage.Path = fileName;
            }
            var existingCount = await _repository.GetItemImageCountAsync(itemImage.DonatedItemId);
            bool isMain = existingCount == 0;
            var itemImageEntity = _mapper.MapToItemImage(itemImage);
            itemImageEntity.IsMain = isMain;
            var create = await _repository.AddItemImageAsync(itemImageEntity);
            return new ServiceResponse<int>
            {
                Success = true,
                Message = "Image added successfully.",
                Data = itemImageEntity.Id
            };
        }

        public async Task<ServiceResponse<bool>> DeleteItemImage(int imageId)
        {
            var itemImage = await _repository.GetItemImageByIdAsync(imageId);
            if (itemImage == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Item image with ID {imageId} not found.",
                    Data = false
                };
            }
            bool isMain = itemImage.IsMain;
            FileManager.RemoveFile("DonatedItems/Images", itemImage.Path);
            if (isMain)
            {
                var otherImages = await _repository.GetItemImagesAsync(itemImage.DonatedItemId.Value);
                var newMainImage = otherImages.FirstOrDefault(img => img.Id != imageId);
                if (newMainImage != null)
                {
                    newMainImage.IsMain = true;
                    await _repository.UpdateItemImageAsync(newMainImage);
                }
            }
            await _repository.DeleteItemImageAsync(imageId);
            return new ServiceResponse<bool>
            {
                Success = true,
                Message = "Image deleted successfully.",
                Data = true
            };
        }

        public async Task<ServiceResponse<ItemImageResponseDto>> GetItemImageById(int imageId)
        {
            var itemImage = await _repository.GetItemImageByIdAsync(imageId);
            if (itemImage == null)
            {
                return new ServiceResponse<ItemImageResponseDto>
                {
                    Success = false,
                    Message = $"Item image with ID {imageId} not found.",
                    Data = null
                };
            }
            var itemImageDto = _mapper.MapToItemImageResponseDto(itemImage);
            return new ServiceResponse<ItemImageResponseDto>
            {
                Success = true,
                Message = "Image retrieved successfully.",
                Data = itemImageDto
            };
        }

        public async Task<ServiceResponse<int>> GetItemImageCount(int donatedItemId)
        {
            var count = await _repository.GetItemImageCountAsync(donatedItemId);
            return new ServiceResponse<int>
            {
                Success = true,
                Message = "Image count retrieved successfully.",
                Data = count
            };
        }

        public async Task<ServiceResponse<IEnumerable<ItemImageResponseDto>>> GetItemImages(int donatedItemId)
        {
            if(!await _repository.DonatedItemExistsAsync(donatedItemId))
            {
                return new ServiceResponse<IEnumerable<ItemImageResponseDto>>
                {
                    Success = false,
                    Message = $"Donated item with ID {donatedItemId} not found.",
                    Data = null
                };
            }
            var itemImages = await _repository.GetItemImagesAsync(donatedItemId);
            var itemImageDtos = _mapper.MapToItemImageResponseDtos(itemImages);
            return new ServiceResponse<IEnumerable<ItemImageResponseDto>>
            {
                Success = true,
                Message = "Images retrieved successfully.",
                Data = itemImageDtos
            };
        }

        public async Task<ServiceResponse<ItemImageResponseDto>> GetPrimaryItemImage(int donatedItemId)
        {
            var itemImage = await _repository.GetPrimaryItemImageAsync(donatedItemId);
            if (itemImage == null)
            {
                return new ServiceResponse<ItemImageResponseDto>
                {
                    Success = false,
                    Message = $"Primary image for donated item with ID {donatedItemId} not found.",
                    Data = null
                };
            }
            var itemImageDto = _mapper.MapToItemImageResponseDto(itemImage);
            return new ServiceResponse<ItemImageResponseDto>
            {
                Success = true,
                Message = "Primary image retrieved successfully.",
                Data = itemImageDto
            };
        }
    }
}
