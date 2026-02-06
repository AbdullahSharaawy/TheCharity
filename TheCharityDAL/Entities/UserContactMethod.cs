using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations.Schema;
using TheCharityDAL.Enums;

namespace TheCharityDAL.Entities
{
    public class UserContactMethod
    {
        public int Id { get; private set; }
        public string? Value { get; private set; }
        public ContactType? Type { get; private set; }
        public bool IsDeleted { get; private set; } = false;
        public DateTime? DeletedOn { get; private set; }
        public DateTime? RegistrationDate { get; private set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; private set; }
        public string? UserId { get; private set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; private set; }
        public UserContactMethod(string? value, ContactType? type, string? userId)
        {
            this.Value = value;
            this.Type = type;
            this.UserId = userId;
        }
        public void EditValue(string? value)
        {
            if (!value.IsNullOrEmpty())
            {
                this.Value = value;
                this.UpdatedOn = DateTime.Now;
            }
        }
        public void EditType(ContactType? type)
        {
            if (type.HasValue)
            {
                this.Type = type;
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
            this.UpdatedOn = DeletedOn;
        }
    }
}
