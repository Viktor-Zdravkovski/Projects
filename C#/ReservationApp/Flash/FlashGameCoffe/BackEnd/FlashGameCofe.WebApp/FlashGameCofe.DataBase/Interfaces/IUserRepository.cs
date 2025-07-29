using FlashGameCofe.Domain.Models;

namespace FlashGameCofe.DataBase.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> LogInUser(string email, string hashPasswrod);

        Task<User> GetUserByUsername(string username);
    }
}
