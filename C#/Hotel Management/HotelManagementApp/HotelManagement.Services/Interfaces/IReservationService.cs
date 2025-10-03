using HotelManagement.Dto.ReservationsDto;
using HotelManagement.Dto.RoomsDto;

namespace HotelManagement.Services.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllReservations();

        Task<ReservationDto> GetReservationById(int id);

        Task<IEnumerable<ReservationDto>> GetReservationsByUser(int userId);

        Task<IEnumerable<RoomDto>> CheckAvailability(DateTime checkIn, DateTime checkOut);

        Task AddReservation(AddReservationDto addReservationDto);

        Task UpdateReservation(int id, UpdateReservationDto updateReservationDto);

        Task CancelReservation(int id);

        //Task DeleteReservation(int id);
    }
}
