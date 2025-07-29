using FlashGameCofe.DataBase.Context;
using FlashGameCofe.DataBase.Interfaces;
using FlashGameCofe.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashGameCofe.DataBase.Implementations.EFImplementations
{
    public class UserRepository : IUserRepository
    {
        private readonly FlashGameCofeDbContext _dbContext;

        public UserRepository(FlashGameCofeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(User entity)
        {
            await _dbContext.Users.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (existingUser != null)
            {
                _dbContext.Entry(existingUser).CurrentValues.SetValues(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<User> LogInUserAsync(string email, string hashPassword)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == hashPassword);
        }

        public async Task DeleteAsync(User entity)
        {
            _dbContext.Users.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> LogInUser(string email, string hashPasswrod)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == hashPasswrod);
        }

        public async Task<User> GetUserByUsername(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
        }
    }
}
