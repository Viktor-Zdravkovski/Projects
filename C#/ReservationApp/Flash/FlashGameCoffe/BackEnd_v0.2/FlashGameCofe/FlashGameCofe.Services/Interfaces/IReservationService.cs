using FlashGameCofe.Domain.Models;
using FlashGameCofe.Dto.ReservationsDto;

namespace FlashGameCofe.Services.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllReservations();

        Task<ReservationDto> GetReservationById(int id);

        Task<IEnumerable<ReservationDto>> GetReservationsByUserIdAsync(int userId);

        Task<IEnumerable<ReservationDto>> GetReservatiosByDate(DateTime date);

        Task<bool> AddOrUpdateReservation(AddReservationDto dto, int userId);

        Task<List<string>> GetReservedSlotsByDateAsync(DateTime date);

        Task AddReservation(AddReservationDto reservation);

        Task UpdateReservation(UpdateReservationDto updateReservationDto);

        Task DeleteReservation(int id);
    }
}
