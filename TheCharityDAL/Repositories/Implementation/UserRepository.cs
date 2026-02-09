using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheCharityDAL.Database;
using TheCharityDAL.Entities;
using TheCharityDAL.Repositories.Abstraction;

namespace TheCharityDAL.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TheCharityDbContext _context;

        public UserRepository(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            TheCharityDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        // Create user with password
        public async Task<IdentityResult> CreateUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        // Update user
        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }

        // Delete user
        public async Task<IdentityResult> DeleteUserAsync(User user)
        {
            user.Delete();
            return await _userManager.UpdateAsync(user);
        }

        // Change password
        public async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        // Add user to role
        public async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            // Ensure role exists
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            return await _userManager.AddToRoleAsync(user, role);
        }

        // Remove user from role
        public async Task<IdentityResult> RemoveFromRoleAsync(User user, string role)
        {
            return await _userManager.RemoveFromRoleAsync(user, role);
        }

        // Get user by ID
        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _userManager.Users
                .Where(u => u.Id == id && !u.IsDeleted)
                .Include(u => u.ContactMethods.Where(cm => !cm.IsDeleted))
                .FirstOrDefaultAsync();
        }

        // Get user by username
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _userManager.Users
                .Where(u => u.UserName == username && !u.IsDeleted)
                .Include(u => u.ContactMethods.Where(cm => !cm.IsDeleted))
                .FirstOrDefaultAsync();
        }

        // Get user by email
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userManager.Users
                .Where(u => u.Email == email && !u.IsDeleted)
                .Include(u => u.ContactMethods.Where(cm => !cm.IsDeleted))
                .FirstOrDefaultAsync();
        }

        // Get all users (excluding deleted)
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userManager.Users
                .Where(u => !u.IsDeleted)
                .Include(u => u.ContactMethods.Where(cm => !cm.IsDeleted))
                .ToListAsync();
        }

        // Get users in a specific role
        public async Task<IEnumerable<User>> GetUsersInRoleAsync(string role)
        {
            return await _userManager.GetUsersInRoleAsync(role);
        }

        // Check password
        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        // Check if user is in role
        public async Task<bool> IsInRoleAsync(User user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }

        // Contact methods (if you still need these)
        public async Task<IEnumerable<UserContactMethod>> GetUserContactMethodsAsync(string userId)
        {
            return await _context.UserContactMethods
                .Where(cm => cm.UserId == userId && !cm.IsDeleted)
                .ToListAsync();
        }

        public async Task<UserContactMethod> AddContactMethodAsync(UserContactMethod contactMethod)
        {
            _context.UserContactMethods.Add(contactMethod);
            await _context.SaveChangesAsync();
            return contactMethod;
        }

        public async Task UpdateContactMethodAsync(UserContactMethod contactMethod)
        {
            _context.Entry(contactMethod).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContactMethodAsync(int contactMethodId)
        {
            var contactMethod = await _context.UserContactMethods
                .FindAsync(contactMethodId);

            if (contactMethod != null)
            {
                contactMethod.Delete();
                await _context.SaveChangesAsync();
            }
        }

        // Token generation and confirmation
        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
            {
                user.Delete();
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task RestoreUserAsync(string id)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
            {
                user.Restore();
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<IEnumerable<User>> GetDeletedUsersAsync()
        {
            return await _userManager.Users
                .Where(u => u.IsDeleted == true)
                .Include(u => u.ContactMethods)
                .ToListAsync();
        }
    }
}
