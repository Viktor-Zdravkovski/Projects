using HotelManagement.Domain.Models;

namespace HotelManagement.DataBase.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<Payment> GetPaymentByReservationIdAsync(int reservationId);

        Task<IEnumerable<Payment>> GetPaymentsByUserAsync(int userId);
    }
}
