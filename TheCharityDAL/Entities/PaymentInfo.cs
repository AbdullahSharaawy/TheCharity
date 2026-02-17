using Microsoft.IdentityModel.Tokens;

namespace TheCharityDAL.Entities
{
    public class PaymentInfo
    {
        public int Id { get; private set; }
        public string? ApiKey { get; private set; }
        public string? IntegrationId { get; private set; }

        public string? IframeId { get; private set; }
        public string? HmacKey { get; private set; }

        public bool IsDeleted { get; private set; } = false;
        public DateTime? DeletedOn { get; private set; }
        public DateTime? RegistrationDate { get; private set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; private set; }
        public PaymentInfo( string? apiKey, string? integrationId, string? iframeId, string? hmacKey)
        {
              ApiKey = apiKey;
            IntegrationId = integrationId;
            IframeId = iframeId;
            HmacKey = hmacKey;
        }
        public void EditApiKey(string clientId)
        {
            if (!clientId.IsNullOrEmpty())
            {
                this.ApiKey = clientId;
                this.UpdatedOn = DateTime.Now;
            }
        }
        public void EditIntegrationId(string secretKey)
        {
            if (!secretKey.IsNullOrEmpty())
            {
                this.IntegrationId = secretKey;
                this.UpdatedOn = DateTime.Now;
            }
        }
        public void EditIframeId(string secretKey)
        {
            if (!secretKey.IsNullOrEmpty())
            {
                this.IframeId = secretKey;
                this.UpdatedOn = DateTime.Now;
            }
        }
        public void EditHmacKey(string secretKey)
        {
            if (!secretKey.IsNullOrEmpty())
            {
                this.HmacKey = secretKey;
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
