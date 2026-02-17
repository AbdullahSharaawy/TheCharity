using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCharityDAL.Enums;

namespace TheCharityBLL.DTOs.UserContactMethodDTOs
{
    public class UserContactMethodResponseDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public ContactType Type { get; set; }
        public string UserId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }

    }
}
