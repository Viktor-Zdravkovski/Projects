using HotelManagement.DataBase.Context;
using HotelManagement.DataBase.Interfaces;
using HotelManagement.Domain.Enums;
using HotelManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.DataBase.Implementations.EFImplementations
{
    public class UserRepository : IUserRepository
    {
        private readonly HotelManagementDbContext _context;

        public UserRepository(HotelManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (existingUser == null)
                throw new KeyNotFoundException($"User with Id {entity.Id} not found. ");
        }

        public async Task DeleteAsync(int id)
        {
            var userToDelete = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userToDelete == null)
                throw new KeyNotFoundException($"User with id {id} not found.");

            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

            if (existingUser == null)
                throw new KeyNotFoundException($"The user with email: {email} not found.");

            return existingUser;
        }

        public async Task<IEnumerable<User>> GetByRoleAsync(string role)
        {
            if (!Enum.TryParse<Roles>(role, true, out var parsedRole))
                throw new ArgumentException($"Invalid role: {role}");

            return await _context.Users.Where(x => x.Role == parsedRole).ToListAsync();
        }

        public async Task<User> LogInUser(string email, string hashPasswrod)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower() && x.Password == hashPasswrod);
        }
    }
}
