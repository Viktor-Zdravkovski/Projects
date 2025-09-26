using HotelManagement.Domain.Models;

namespace HotelManagement.DataBase.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<Payment> GetByReservationIdAsync(int reservationId);
    }
}
