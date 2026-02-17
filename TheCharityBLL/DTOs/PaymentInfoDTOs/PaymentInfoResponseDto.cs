using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCharityBLL.DTOs.PaymentInfoDTOs
{
    public class PaymentInfoResponseDto
    {
        public int Id { get; set; }
        public string ApiKey { get; set; } = null!;
        public string IntegrationId { get; set; } = null!;
        public string IframeId { get; set; } = null!;
        public string HmacKey { get; set; } = null!;
        public DateTime? RegistrationDate { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }

    }
}
