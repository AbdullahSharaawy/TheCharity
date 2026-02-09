using Microsoft.AspNetCore.Identity;
using TheCharityDAL.Entities;

namespace TheCharityDAL.Repositories.Abstraction
{
    public interface IUserRepository
    {
        // Identity-based methods (returns IdentityResult)
        Task<IdentityResult> CreateUserAsync(User user, string password);
        Task<IdentityResult> UpdateUserAsync(User user);

        // Original signature methods
        Task DeleteUserAsync(string id);
        Task RestoreUserAsync(string id);

        // Other methods...
        Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);
        Task<IdentityResult> AddToRoleAsync(User user, string role);
        Task<IdentityResult> RemoveFromRoleAsync(User user, string role);

        // Query methods
        Task<User?> GetUserByIdAsync(string id);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> GetDeletedUsersAsync();
        Task<IEnumerable<User>> GetUsersInRoleAsync(string role);

        // Check methods
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<bool> IsInRoleAsync(User user, string role);

        // Contact methods
        Task<IEnumerable<UserContactMethod>> GetUserContactMethodsAsync(string userId);
        Task<UserContactMethod> AddContactMethodAsync(UserContactMethod contactMethod);
        Task UpdateContactMethodAsync(UserContactMethod contactMethod);
        Task DeleteContactMethodAsync(int contactMethodId);

        // Token generation
        Task<string> GenerateEmailConfirmationTokenAsync(User user);
        Task<string> GeneratePasswordResetTokenAsync(User user);
        Task<IdentityResult> ConfirmEmailAsync(User user, string token);
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
    }
}
