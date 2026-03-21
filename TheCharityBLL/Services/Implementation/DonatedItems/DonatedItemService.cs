using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.DonatedItemDTOs;
using TheCharityBLL.Helpers;
using TheCharityBLL.Mapper;
using TheCharityBLL.Services.Abstraction.DonatedItems;
using TheCharityDAL.Entities;
using TheCharityDAL.Enums;
using TheCharityDAL.Repositories.Abstraction;

namespace TheCharityBLL.Services.Implementation.DonatedItems
{
    public class DonatedItemService : IDonatedItemService
    {
        private readonly IDonatedItemsRepository _repository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly DontedItemMapper _mapper;
        public DonatedItemService(IDonatedItemsRepository repository, IOrganizationRepository organizationRepository)
        {
            _repository = repository;
            _mapper = new DontedItemMapper();
            _organizationRepository = organizationRepository;
        }
        public async Task<ServiceResponse<int>> AddDonatedItem(CreateDonatedItemDto donatedItem)
        {
            if (!await _repository.IsDonorAsync(donatedItem.DonorId))
            {
                return new ServiceResponse<int>()
                {
                    Success = false,
                    Message = $"Donor with ID {donatedItem.DonorId} not found.",
                };
            }
            var donatedItemEntity = _mapper.MapToDonatedItem(donatedItem);
            if (donatedItem.ImageFiles != null && donatedItem.ImageFiles.Any())
            {
                for (var i = 0; i < donatedItem.ImageFiles.Count; i++)
                {
                    var fileName = FileManager.UploadFile("DonatedItems/Images", donatedItem.ImageFiles[i]);
                    if (fileName.StartsWith("/"))
                    {
                        bool isMain = i == 0;
                        var itemImage = new ItemImage(fileName, null) { IsMain = isMain };
                        donatedItemEntity.AddImage(itemImage);
                    }

                }
            }
            if (donatedItem.AttachmentFiles != null && donatedItem.AttachmentFiles.Any())
            {
                foreach (var attachment in donatedItem.AttachmentFiles)
                {
                    var fileName = FileManager.UploadFile("DonatedItems/Attachments", attachment);
                    if (fileName.StartsWith("/"))
                    {
                        var itemAttachment = new Attachment(
                    donatedItemId: null,
                    name: attachment.FileName,
                    path: fileName,
                    fileSize: attachment.Length,
                    contentType: attachment.ContentType,
                    isItemAttachment: true
            );

                        donatedItemEntity.AddItemAttachment(itemAttachment);
                    }

                }
            }

            var create = await _repository.AddDonatedItemAsync(donatedItemEntity);
            return new ServiceResponse<int>()
            {
                Success = true,
                Data = create.Id,
                Message = "Donated Item with images and attachments added successfully."
            };
        }

        public async Task<ServiceResponse<bool>> DeleteDonatedItem(int id)
        {
            if (!await _repository.DonatedItemExistsAsync(id))
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Donated Item with ID {id} not found.",
                };
            }
            //soft delete=>so we dont delete images and attachments
            await _repository.DeleteDonatedItemAsync(id);
            return new ServiceResponse<bool>
            {
                Success = true,
                Message = "Donated Item deleted successfully."
            };
        }

        public async Task<ServiceResponse<IEnumerable<DonatedItemResponseDto>>> GetAllDonatedItems(bool includeDeleted)
        {
            var donatedItems = await _repository.GetAllDonatedItemsAsync(includeDeleted);
            var donatedItemsDtos = _mapper.MapToDonatedItemResponseDtos(donatedItems);
            return new ServiceResponse<IEnumerable<DonatedItemResponseDto>>
            {
                Success = true,
                Data = donatedItemsDtos,
                Message = "Donated Items retrieved successfully."
            };
        }

        public async Task<ServiceResponse<DonatedItemDetailsResponseDto>> GetDonatedItemByIdWithDetails(int id)
        {
            var donatedItem = await _repository.GetDonatedItemByIdAsync(id);
            if (donatedItem == null)
            {
                return new ServiceResponse<DonatedItemDetailsResponseDto>
                {
                    Success = false,
                    Message = $"Donated Item with ID {id} not found.",
                };
            }
            var donatedItemDto = _mapper.MapToDonatedItemDetailsResponseDto(donatedItem);
            return new ServiceResponse<DonatedItemDetailsResponseDto>
            {
                Success = true,
                Data = donatedItemDto,
                Message = "Donated Item retrieved successfully."
            };
        }
        public async Task<ServiceResponse<DonatedItemResponseDto>> GetDonatedItemById(int id)
        {
            var donatedItem = await _repository.GetDonatedItemByIdAsync(id);
            if (donatedItem == null)
            {
                return new ServiceResponse<DonatedItemResponseDto>
                {
                    Success = false,
                    Message = $"Donated Item with ID {id} not found.",
                };
            }
            var donatedItemDto = _mapper.MapToDonatedItemResponseDto(donatedItem);
            return new ServiceResponse<DonatedItemResponseDto>
            {
                Success = true,
                Data = donatedItemDto,
                Message = "Donated Item retrieved successfully."
            };
        }
        public async Task<ServiceResponse<bool>> RestoreDonatedItem(int id)
        {
            if (!await _repository.DonatedItemExistsAsync(id))
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Donated Item with ID {id} not found.",
                };
            }
            await _repository.RestoreDonatedItemAsync(id);
            return new ServiceResponse<bool>
            {
                Success = true,
                Message = "Donated Item restored successfully."
            };
        }

        public async Task<ServiceResponse<bool>> TransferItemToOrganization(int itemId, int newOrganizationId)
        {
            if (!await _repository.DonatedItemExistsAsync(itemId))
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Donated Item with ID {itemId} not found.",
                };
            }
            if (!await _organizationRepository.OrganizationExistsAsync(newOrganizationId))
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Organization with ID {newOrganizationId} not found.",
                };
            }
            var transfer = await _repository.TransferItemToOrganizationAsync(itemId, newOrganizationId);
            return new ServiceResponse<bool>
            {
                Success = true,
                Data = true,
                Message = "Donated Item transferred successfully."
            };
        }

        public async Task<ServiceResponse<bool>> UpdateDonatedItem(UpdateDonatedItemDto donatedItem)
        {
            var existingItem = await _repository.GetDonatedItemByIdAsync(donatedItem.Id);
            if (existingItem == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Donated Item with ID {donatedItem.Id} not found.",
                };
            }
            existingItem.EditAvailability(donatedItem.IsAvailable ?? existingItem.IsAvailable);
            existingItem.EditDescription(donatedItem.Description ?? existingItem.Description);
            existingItem.EditItemCategory(donatedItem.ItemCategory ?? existingItem.ItemCategory);
            existingItem.EditName(donatedItem.Name ?? existingItem.Name);
            existingItem.EditOrganization(donatedItem.OrganizationId ?? existingItem.OrganizationId);
            var update = await _repository.UpdateDonatedItemAsync(existingItem);
            return new ServiceResponse<bool>
            {
                Success = true,
                Message = "Donated Item updated successfully."
            };

        }

        public async Task<ServiceResponse<bool>> UpdateItemAvailability(int itemId, bool isAvailable)
        {
            if (!await _repository.DonatedItemExistsAsync(itemId))
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Donated Item with ID {itemId} not found.",
                };
            }
            var update = await _repository.UpdateItemAvailabilityAsync(itemId, isAvailable);
            return new ServiceResponse<bool>
            {
                Success = true,
                Data = true,
                Message = "Donated Item availability updated successfully."
            };
        }

        public async Task<ServiceResponse<bool>> UpdateItemCategory(int itemId, ItemCategory category)
        {
            if (!await _repository.DonatedItemExistsAsync(itemId))
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Donated Item with ID {itemId} not found.",
                };
            }
            var update = await _repository.UpdateItemCategoryAsync(itemId, category);
            return new ServiceResponse<bool>
            {
                Success = true,
                Message = "Donated Item category updated successfully."
            };
        }

        public async Task<ServiceResponse<bool>> UpdateItemDonor(int itemId, string newDonorId)
        {
            if (!await _repository.DonatedItemExistsAsync(itemId))
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Donated Item with ID {itemId} not found.",
                };
            }
            if (!await _repository.IsDonorAsync(newDonorId))
            {
                return new ServiceResponse<bool>()
                {
                    Success = false,
                    Message = $"Donor with ID {newDonorId} not found.",
                };
            }
            var update = await _repository.UpdateItemDonorAsync(itemId, newDonorId);
            return new ServiceResponse<bool>
            {
                Success = true,
                Data = true,
                Message = "Donated Item donor updated successfully."
            };
        }

        public async Task<ServiceResponse<int>> BulkUpdateItemCategories(ItemCategory oldCategory, ItemCategory newCategory)
        {
            var affectedRows = await _repository.BulkUpdateItemCategoriesAsync(oldCategory, newCategory);
            return new ServiceResponse<int>
            {
                Success = true,
                Data = affectedRows,
                Message = $"Successfully updated {affectedRows} items from {oldCategory} to {newCategory}."
            };
        }

        public async Task<ServiceResponse<int>> BulkMarkItemsAsUnavailable(int organizationId)
        {
            if (!await _organizationRepository.OrganizationExistsAsync(organizationId))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = $"Organization with ID {organizationId} not found.",
                };
            }
            var affectedRows = await _repository.BulkMarkItemsAsUnavailableAsync(organizationId);
            return new ServiceResponse<int>
            {
                Success = true,
                Data = affectedRows,
                Message = $"Marked {affectedRows} items as unavailable for organization {organizationId}."
            };
        }

        public async Task<ServiceResponse<int>> DeleteOldDonatedItems(int daysOld)
        {
            var delete = await _repository.DeleteOldDonatedItemsAsync(daysOld);
            return new ServiceResponse<int>
            {
                Success = true,
                Data = delete,
                Message = "Old Donated Items deleted successfully."
            };
        }
    }
}
