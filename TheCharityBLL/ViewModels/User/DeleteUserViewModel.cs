using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCharityBLL.ViewModels.User
{
    public class DeleteUserViewModel
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public DateTime? RegistrationDate { get; set; }
    }
}
