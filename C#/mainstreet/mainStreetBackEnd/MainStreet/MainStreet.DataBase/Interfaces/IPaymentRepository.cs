using MainStreet.Domain.Models;

namespace MainStreet.DataBase.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<decimal> GetTotalRevenueByDateAsync(DateTime dateTime);

        Task<Payment?> GetByOrderIdAsync(int orderId);

        Task<IEnumerable<Payment>> GetPaymentsByCertainDateAsync(DateTime date);

        Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(int userId);
    }
}
