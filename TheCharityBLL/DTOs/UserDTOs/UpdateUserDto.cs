using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCharityBLL.DTOs.AccountDTOs
{
    public class UpdateUserDto
    {

        [MaxLength(100, ErrorMessage = "Username cannot exceed 100 characters.")]
        public string? FullName { get; set; }

        [MaxLength(1000, ErrorMessage = "Image path cannot exceed 1000 characters.")]
        public string? ImgPath { get; set; }

        [MaxLength(500, ErrorMessage = "Address cannot exceed 500 characters.")]
        public string? Address { get; set; }
    }
}
