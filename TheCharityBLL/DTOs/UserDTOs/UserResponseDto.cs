using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCharityBLL.DTOs.UserContactMethodDTOs;

namespace TheCharityBLL.DTOs.AccountDTOs
{
    public class UserResponseDto
    {
        public string Id { get; set; } = null!;
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? ImgPath { get; set; }
        public string? Address { get; set; }
        public List<UserContactMethodResponseDto>? ContactMethods { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
