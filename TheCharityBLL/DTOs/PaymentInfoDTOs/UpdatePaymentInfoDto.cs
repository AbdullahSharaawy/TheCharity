using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCharityBLL.DTOs.PaymentInfoDTOs
{
    public class UpdatePaymentInfoDto
    {

        [MaxLength(300, ErrorMessage = "ApiKey cannot exceed 300 characters.")]
        public string? ApiKey { get; set; } = null!;

        [MaxLength(300, ErrorMessage = "IntegrationId cannot exceed 300 characters.")]
        public string? IntegrationId { get; set; } = null!;

        [MaxLength(300, ErrorMessage = "IframeId cannot exceed 300 characters.")]
        public string? IframeId { get; set; } = null!;

        [MaxLength(300, ErrorMessage = "HmacKey cannot exceed 300 characters.")]
        public string? HmacKey { get; set; } = null!;
    }
}
