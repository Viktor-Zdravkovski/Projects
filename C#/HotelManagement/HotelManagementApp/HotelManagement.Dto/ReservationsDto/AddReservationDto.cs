using HotelManagement.Domain.Enums;
using HotelManagement.Domain.Models;

namespace HotelManagement.Dto.ReservationsDto
{
    public class AddReservationDto
    {
        public int UserId { get; set; }

        public int RoomId { get; set; }

        public DateTime CheckedIn { get; set; }
        public DateTime CheckedOut { get; set; }

    }
}
