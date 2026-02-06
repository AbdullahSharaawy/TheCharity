using TheCharityDAL.Entities;

namespace TheCharityDAL.Repositories.Abstraction
{
    public interface IUserRepository
    {
        public interface IUserRepository
        {
            Task<IEnumerable<User>> GetAllUsersAsync();
            Task<User?> GetUserByIdAsync(string id);
            Task<User?> GetUserByUsernameAsync(string username);
            Task<User?> GetUserByEmailAsync(string email);
            Task<IEnumerable<User>> GetDeletedUsersAsync();
            Task<User> AddUserAsync(User user);
            Task<User> UpdateUserAsync(User user);
            Task DeleteUserAsync(string id);
            Task RestoreUserAsync(string id);
            Task<IEnumerable<UserContactMethod>> GetUserContactMethodsAsync(string userId);
            Task<UserContactMethod> AddContactMethodAsync(UserContactMethod contactMethod);
            Task UpdateContactMethodAsync(UserContactMethod contactMethod);
            Task DeleteContactMethodAsync(int contactMethodId);
        }
    }
}
