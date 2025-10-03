using HotelManagement.Domain.Enums;
using HotelManagement.Domain.Models;

namespace HotelManagement.Dto.ReservationsDto
{
    public class UpdateReservationDto
    {
        public int RoomId { get; set; }

        public DateTime CheckedIn { get; set; }
        public DateTime CheckedOut { get; set; }

    }
}
