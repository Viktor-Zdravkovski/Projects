using FlashGameCofe.Domain.Models;

namespace FlashGameCofe.DataBase.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int userId);

        Task<IEnumerable<Reservation>> GetByDateAsync(DateTime date);

        Task<List<string>> GetReservedSlotsByDateAsync(DateTime date);

        Task<Reservation?> GetLatestReservationByUserId(int userId);

        Task<IEnumerable<Reservation>> GetAllReservationsWithUsers();
    }
}
