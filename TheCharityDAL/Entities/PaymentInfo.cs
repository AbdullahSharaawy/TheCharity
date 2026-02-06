using Microsoft.IdentityModel.Tokens;

namespace TheCharityDAL.Entities
{
    public class PaymentInfo
    {
        public int Id { get; private set; }
        public string? ClientId { get; private set; }
        public string? SecretKey { get; private set; }
        public bool IsDeleted { get; private set; } = false;
        public DateTime? DeletedOn { get; private set; }
        public DateTime? RegistrationDate { get; private set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; private set; }
        public PaymentInfo(string clientId, string secretKey)
        {
            this.ClientId = clientId;
            this.SecretKey = secretKey;
        }
        public void EditClientId(string clientId)
        {
            if (!clientId.IsNullOrEmpty())
            {
                this.ClientId = clientId;
                this.UpdatedOn = DateTime.Now;
            }
        }
        public void EditSecretKey(string secretKey)
        {
            if (!secretKey.IsNullOrEmpty())
            {
                this.SecretKey = secretKey;
                this.UpdatedOn = DateTime.Now;
            }
        }
        public void Delete() { 
            this.IsDeleted = true;
            this.DeletedOn = DateTime.Now;
        }
        public void Restore() { 
            this.IsDeleted = false;
            this.UpdatedOn = DateTime.Now;
        }
    }
}
