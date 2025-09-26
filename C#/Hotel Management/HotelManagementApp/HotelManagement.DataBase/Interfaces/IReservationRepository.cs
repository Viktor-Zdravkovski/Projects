using HotelManagement.Domain.Models;

namespace HotelManagement.DataBase.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetReservationsByUserAsync(int userId);

        Task<IEnumerable<Reservation>> GetReservationsByDateAsync(DateTime date);

        Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut);
    }
}
