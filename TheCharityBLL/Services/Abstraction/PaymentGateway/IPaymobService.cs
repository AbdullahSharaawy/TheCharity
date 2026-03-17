using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCharityBLL.Services.Abstraction.Payment
{
    public interface IPaymobService
    {
        public Task<string> CreatePayment(decimal amount, string currency = "EGP");
    }
}
