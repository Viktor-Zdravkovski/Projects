using MainStreet.DataBase.Context;
using MainStreet.DataBase.Interfaces;
using MainStreet.Domain.Enums;
using MainStreet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MainStreet.DataBase.Implementations.EFImplementations
{
    public class UserRepository : IUserRepository
    {
        private readonly MainStreetDbContext _dbContext;

        public UserRepository(MainStreetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetIdByAsync(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(User entity)
        {
            User? existingUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (existingUser != null)
            {
                _dbContext.Entry(existingUser).CurrentValues.SetValues(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task AddAsync(User entity)
        {
            await _dbContext.Users.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User entity)
        {
            _dbContext.Users.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> LogInUserAsync(string email, string hashPassword)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == hashPassword);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(Roles role)
        {
            return await _dbContext.Users.AsNoTracking().Where(x => x.Role == role).ToListAsync();
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return !await _dbContext.Users.AnyAsync(x => x.Email == email);
        }
    }
}
