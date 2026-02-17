using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCharityBLL.DTOs.AttachmentDTOs
{
    public class AttachmentResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Path { get; set; }
        public long? FileSize { get; set; }
        public string? ContentType { get; set; }
        public bool IsItemAttachment { get; set; }
        public int DonatedItemId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }

    }
}
