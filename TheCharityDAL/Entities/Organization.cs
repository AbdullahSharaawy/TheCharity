using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheCharityDAL.Entities
{
    public class Organization
    {
        public int Id { get; private set; }
        public string? Name { get; private set; }
        public string? Address { get; private set; }
        public int? PaymentId { get; private set; }

        [ForeignKey(nameof(PaymentId))]
        public PaymentInfo? PaymentInfo { get; private set; }
        public virtual ICollection<Campaign> Campaigns { get; private set; } = new List<Campaign>();
        public virtual ICollection<OrganizationContactMethod> ContactMethods { get; private set; } = new List<OrganizationContactMethod>();
        public bool IsDeleted { get; private set; } = false;
        public DateTime? DeletedOn { get; private set; }
        public DateTime? RegistrationDate { get; private set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; private set; }
        public Organization(string? name, string? address, int? paymentId)
        {
            this.Name = name;
            this.Address = address;
            this.PaymentId = paymentId;
        }
        public void EditName(string? name)
        {
            if (!name.IsNullOrEmpty())
            {
                this.Name = name;
                this.UpdatedOn = DateTime.Now;
            }
        }
        public void EditAddress(string? address)
        {
            if (!address.IsNullOrEmpty())
            {
                this.Address = address;
                this.UpdatedOn = DateTime.Now;
            }
        }
        public void Delete()
        {
            this.IsDeleted = true;
            this.DeletedOn = DateTime.Now;
        }
        public void Restore()
        {
            this.IsDeleted = false;
            this.UpdatedOn = DateTime.Now;
        }
    }
}
