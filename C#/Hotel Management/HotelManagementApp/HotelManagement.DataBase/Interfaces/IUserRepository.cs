using HotelManagement.Domain.Models;

namespace HotelManagement.DataBase.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);

        Task<IEnumerable<User>> GetByRoleAsync(string role);

        Task<User> LogInUser(string email, string hashPasswrod);
    }
}
