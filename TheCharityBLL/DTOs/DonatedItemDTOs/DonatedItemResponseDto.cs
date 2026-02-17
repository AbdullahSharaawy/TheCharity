using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCharityBLL.DTOs.AttachmentDTOs;
using TheCharityBLL.DTOs.ItemImageDTOs;
using TheCharityDAL.Enums;

namespace TheCharityBLL.DTOs.DonatedItemDTOs
{
    public class DonatedItemResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ItemCategory ItemCategory { get; set; }
        public bool IsAvailable { get; set; }
        public int OrganizationId { get; set; }
        public string DonorId { get; set; } = null!;
        public List<ItemImageResponseDto>? Images { get; set; }
        public List<AttachmentResponseDto>? ItemAttachments { get; set; }
        public List<AttachmentResponseDto>? RecipientAttachments { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }

    }
}
