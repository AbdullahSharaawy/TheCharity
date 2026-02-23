
using TheCharityBLL.DTOs;
using TheCharityBLL.DTOs.AttachmentDTOs;

namespace TheCharityBLL.Services.Abstraction
{
    public interface IDonatedItemAttachmentService
    {
        Task<ServiceResponse<IEnumerable<AttachmentResponseDto>>> GetItemAttachments(int donatedItemId);
        Task<ServiceResponse<IEnumerable<AttachmentResponseDto>>> GetRecipientAttachments(int donatedItemId);
        Task<ServiceResponse<IEnumerable<AttachmentResponseDto>>> GetAllAttachments(int donatedItemId);
        Task<ServiceResponse<AttachmentResponseDto>> GetAttachmentById(int attachmentId);
        Task<ServiceResponse<int>> AddAttachment(CreateAttachmentDto attachment);
        //Task<ServiceResponse<bool>> UpdateAttachment(int id,UpdateAttachmentDto attachment);
        Task<ServiceResponse<bool>> DeleteAttachment(int attachmentId);
    }
}
