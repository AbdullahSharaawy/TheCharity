using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCharityBLL.ViewModels.User
{
    public class ConfirmEmailViewModel
    {
        public string email { get; set; }
        public string encodedToken { get; set; }
    }
}
