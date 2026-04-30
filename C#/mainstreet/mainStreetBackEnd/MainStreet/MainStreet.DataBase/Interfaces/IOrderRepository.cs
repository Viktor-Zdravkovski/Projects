using MainStreet.Domain.Models;

namespace MainStreet.DataBase.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetOrderWithDetailsAsync(int orderId);

        Task<IEnumerable<Order>> GetActiveOrdersAsync();

        Task<IEnumerable<Order>> GetRecentOrdersByUserIdAsync(int userId, int count);

        Task<IEnumerable<Order>> SearchByNameAsync(string name);

        Task<IEnumerable<Order>> GetOrdersByCertainDateAsync(DateTime date);
    }
}
