using HotelManagement.Domain.Enums;
using HotelManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.DataBase.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<Payment> GetPaymentByReservationIdAsync(int reservationId);

        Task<IEnumerable<Payment>> GetPaymentsByUserAsync(int userId);

        Task<Payment?> GetByReservationIdAsync(int reservationId);

        Task<Payment?> GetCompletedPaymentByReservationIdAsync(int reservationId);

    }
}
