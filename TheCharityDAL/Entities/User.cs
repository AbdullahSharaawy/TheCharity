using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace TheCharityDAL.Entities
{
    public class User : IdentityUser
    {
        public string? ImgPath { get; private set; }
        public bool IsDeleted { get; private set; } = false;
        public string? FullName { get; private set; } = "User";
        public DateTime? DeletedOn { get; private set; }
        public DateTime? RegistrationDate { get; private set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; private set; }
        public string? Address { get; private set; }
        public virtual ICollection<UserContactMethod> ContactMethods { get; private set; } = new List<UserContactMethod>();
        public long StorageOwned { get; private set; } = 2000 * 1024 * 1024; // in bytes
        public DateTime? LastStorageUpdate { get; private set; }


        public User(string? userName,string? FullName, string? email, string? imgPath)
        {
            this.UserName = userName;// بقلك ايه انا ناقله من كريستين كدا
            this.Email = email;
            this.ImgPath = imgPath;
            this.FullName = FullName;
        }
        public void EditUsername(string? userName)
        {
            if (!userName.IsNullOrEmpty())
            {
                this.FullName = userName;
                this.UpdatedOn = DateTime.Now;
            }
        }
        public void EditImage(string? imgPath)
        {
            if (!imgPath.IsNullOrEmpty())
            {
                this.ImgPath = imgPath;
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
            DeletedOn = DateTime.Now;
        }
        public void Restore()
        {
            this.IsDeleted = false;
            UpdatedOn = DateTime.Now;
        }
    }
}
