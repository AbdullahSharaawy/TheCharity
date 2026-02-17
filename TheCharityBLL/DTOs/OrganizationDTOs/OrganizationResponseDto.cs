using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCharityBLL.DTOs.OrganizationContactMethodDTOs;

namespace TheCharityBLL.DTOs.OrganizationDTOs
{
    public class OrganizationResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? PaymentId { get; set; }
        public List<OrganizationContactMethodResponseDto>? ContactMethods { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
