using MainStreet.Domain.Enums;
using MainStreet.Domain.Models;

namespace MainStreet.DataBase.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> LogInUserAsync(string email, string hashPassword);

        Task<User> GetUserByEmailAsync(string email);

        Task<IEnumerable<User>> GetUsersByRoleAsync(Roles role);

        Task<bool> IsEmailUniqueAsync(string email);
    }
}
