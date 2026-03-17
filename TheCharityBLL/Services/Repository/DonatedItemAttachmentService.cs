
using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.AttachmentDTOs;
using TheCharityBLL.Helpers;
using TheCharityBLL.Mapper;
using TheCharityBLL.Services.Abstraction.DonatedItems;
using TheCharityDAL.Entities;
using TheCharityDAL.Repositories.Abstraction;

namespace TheCharityBLL.Services.Repository
{
    public class DonatedItemAttachmentService : IDonatedItemAttachmentService
    {
        private readonly IDonatedItemsRepository _repository;
        private readonly AttachmentMapper _mapper;
        public DonatedItemAttachmentService(IDonatedItemsRepository repository)
        {
            _repository = repository;
            _mapper = new AttachmentMapper();
        }
        public async Task<ServiceResponse<int>> AddAttachment(CreateAttachmentDto attachment)
        {
            if (!await _repository.DonatedItemExistsAsync(attachment.DonatedItemId))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = $"Donated item with id {attachment.DonatedItemId} not found.",
                };
            }
            if (attachment.FileUrl != null && attachment.FileUrl.Length > 0)
            {
                var fileName = FileManager.UploadFile("DonatedItems/Attachments", attachment.FileUrl);
                if (!fileName.StartsWith("/"))
                {
                    return new ServiceResponse<int>
                    {
                        Success = false,
                        Message = "File upload failed: " + fileName,
                    };
                }
                attachment.Path = fileName;

            }
            var attachmentEntity = _mapper.MapToAttachment(attachment);
            var create=await _repository.AddAttachmentAsync(attachmentEntity);
            return new ServiceResponse<int>
            {
                Success = true,
                Message = "Attachment added successfully.",
                Data = attachmentEntity.Id
            };

        }

        public async Task<ServiceResponse<bool>> DeleteAttachment(int attachmentId)
        {
            var attachment =await _repository.GetAttachmentByIdAsync(attachmentId);
            if(attachment == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Attachment with id {attachmentId} not found.",
                    Data = false
                };
            }
            FileManager.RemoveFile("DonatedItems/Attachments", attachment.Path);
            await _repository.DeleteAttachmentAsync(attachmentId);
            return new ServiceResponse<bool>
            {
                Success = true,
                Message = "Attachment deleted successfully.",
                Data = true
            };

        }

        public async Task<ServiceResponse<IEnumerable<AttachmentResponseDto>>> GetAllAttachments(int donatedItemId)
        {
            if(await _repository.DonatedItemExistsAsync(donatedItemId) == false)
            {
                return new ServiceResponse<IEnumerable<AttachmentResponseDto>>
                {
                    Success = false,
                    Message = $"Donated item with id {donatedItemId} not found.",
                };
            }
            var attachments =await _repository.GetAllAttachmentsAsync(donatedItemId);
            var attachmentsDto = _mapper.MapToAttachmentResponseDtos(attachments);
            return new ServiceResponse<IEnumerable<AttachmentResponseDto>>
            {
                Success = true,
                Message = "Attachments retrieved successfully.",
                Data = attachmentsDto
            };
        }

        public async Task<ServiceResponse<AttachmentResponseDto>> GetAttachmentById(int attachmentId)
        {
            var attachment =await _repository.GetAttachmentByIdAsync(attachmentId);
            if(attachment == null)
            {
                return new ServiceResponse<AttachmentResponseDto>
                {
                    Success = false,
                    Message = $"Attachment with id {attachmentId} not found.",
                };
            }
            var attachmentDto = _mapper.MapToAttachmentResponseDto(attachment);
            return new ServiceResponse<AttachmentResponseDto>
            {
                Success = true,
                Message = "Attachment retrieved successfully.",
                Data = attachmentDto
            };
        }

        public async Task<ServiceResponse<IEnumerable<AttachmentResponseDto>>> GetItemAttachments(int donatedItemId)
        {
            if(await _repository.DonatedItemExistsAsync(donatedItemId) == false)
            {
                return new ServiceResponse<IEnumerable<AttachmentResponseDto>>
                {
                    Success = false,
                    Message = $"Donated item with id {donatedItemId} not found.",
                };
            }
            var attachments =await _repository.GetItemAttachmentsAsync(donatedItemId);
            var attachmentsDto = _mapper.MapToAttachmentResponseDtos(attachments);
            return new ServiceResponse<IEnumerable<AttachmentResponseDto>>
            {
                Success = true,
                Message = "Item attachments retrieved successfully.",
                Data = attachmentsDto
            };
        }

        public async Task<ServiceResponse<IEnumerable<AttachmentResponseDto>>> GetRecipientAttachments(int donatedItemId)
        {
            if(await _repository.DonatedItemExistsAsync(donatedItemId) == false)
            {
                return new ServiceResponse<IEnumerable<AttachmentResponseDto>>
                {
                    Success = false,
                    Message = $"Donated item with id {donatedItemId} not found.",
                };
            }
            var attachments =await _repository.GetRecipientAttachmentsAsync(donatedItemId);
            var attachmentsDto = _mapper.MapToAttachmentResponseDtos(attachments);
            return new ServiceResponse<IEnumerable<AttachmentResponseDto>>
            {
                Success = true,
                Message = "Recipient attachments retrieved successfully.",
                Data = attachmentsDto
            };
        }

        //public Task<ServiceResponse<bool>> UpdateAttachment(int id, UpdateAttachmentDto attachment)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
