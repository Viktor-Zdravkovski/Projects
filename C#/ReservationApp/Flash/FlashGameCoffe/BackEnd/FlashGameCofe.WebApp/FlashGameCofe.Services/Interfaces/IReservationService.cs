using FlashGameCofe.Dto.ReservationDto;

namespace FlashGameCofe.Services.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationsDto>> GetAllReservations();

        Task<ReservationsDto> GetReservationById(int id);

        Task AddReservation(AddReservationsDto reservation);

        Task UpdateReservation(UpdateReservationDto updateReservationDto);

        Task DeleteReservation(int id);

    }
}
