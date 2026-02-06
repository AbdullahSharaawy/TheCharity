using Microsoft.EntityFrameworkCore;
using TheCharityDAL.Database;
using TheCharityDAL.Entities;
using TheCharityDAL.Repositories.Abstraction;

namespace TheCharityDAL.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly TheCharityDbContext _context;

        public UserRepository(TheCharityDbContext context)
        {
            _context = context;
        }

        // Get all users (excluding deleted ones by default)
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Where(u => u.IsDeleted == false)
                .Include(u => u.ContactMethods.Where(cm => cm.IsDeleted == false))
                .ToListAsync();
        }

        // Get user by ID
        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _context.Users
                .Where(u => u.Id == id && u.IsDeleted == false)
                .Include(u => u.ContactMethods.Where(cm => cm.IsDeleted == false))
                .FirstOrDefaultAsync();
        }

        // Get user by username
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Where(u => u.UserName == username && u.IsDeleted == false)
                .Include(u => u.ContactMethods.Where(cm => cm.IsDeleted == false))
                .FirstOrDefaultAsync();
        }

        // Get user by email
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .Where(u => u.Email == email && u.IsDeleted == false)
                .Include(u => u.ContactMethods.Where(cm => cm.IsDeleted == false))
                .FirstOrDefaultAsync();
        }

        // Get all deleted users
        public async Task<IEnumerable<User>> GetDeletedUsersAsync()
        {
            return await _context.Users
                .Where(u => u.IsDeleted == true)
                .Include(u => u.ContactMethods)
                .ToListAsync();
        }

        // Add new user
        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Update user information
        public async Task<User> UpdateUserAsync(User user)
        {
            // Mark as modified
            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return user;
        }

        // Soft delete user
        public async Task DeleteUserAsync(string id)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
            {
                user.Delete();
                await _context.SaveChangesAsync();
            }
        }

        // Restore deleted user
        public async Task RestoreUserAsync(string id)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
            {
                user.Restore();
                await _context.SaveChangesAsync();
            }
        }

        // Get user contact methods
        public async Task<IEnumerable<UserContactMethod>> GetUserContactMethodsAsync(string userId)
        {
            return await _context.UserContactMethods
                .Where(cm => cm.UserId == userId && cm.IsDeleted == false)
                .ToListAsync();
        }

        // Add contact method
        public async Task<UserContactMethod> AddContactMethodAsync(UserContactMethod contactMethod)
        {
            _context.UserContactMethods.Add(contactMethod);
            await _context.SaveChangesAsync();
            return contactMethod;
        }

        // Update contact method
        public async Task UpdateContactMethodAsync(UserContactMethod contactMethod)
        {
            _context.Entry(contactMethod).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Delete contact method (soft delete)
        public async Task DeleteContactMethodAsync(int contactMethodId)
        {
            var contactMethod = await _context.UserContactMethods
                .Where(cm => cm.Id == contactMethodId)
                .FirstOrDefaultAsync();

            if (contactMethod != null)
            {
                contactMethod.Delete();
                await _context.SaveChangesAsync();
            }
        }
    }
}
