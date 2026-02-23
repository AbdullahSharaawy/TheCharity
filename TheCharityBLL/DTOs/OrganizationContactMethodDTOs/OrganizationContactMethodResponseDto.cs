
using TheCharityDAL.Enums;

namespace TheCharityBLL.DTOs.OrganizationContactMethodDTOs
{
    public class OrganizationContactMethodResponseDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public ContactType Type { get; set; }
        public int CompanyId { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime RegistrationDate { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
