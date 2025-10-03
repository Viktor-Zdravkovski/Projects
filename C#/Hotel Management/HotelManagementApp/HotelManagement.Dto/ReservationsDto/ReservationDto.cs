using HotelManagement.Domain.Enums;
using HotelManagement.Domain.Models;
using HotelManagement.Dto.RoomsDto;
using HotelManagement.Dto.UsersDto;

namespace HotelManagement.Dto.ReservationsDto
{
    public class ReservationDto
    {
        public UserDto User { get; set; }

        public RoomDto Room { get; set; }

        public DateTime CheckedIn { get; set; }
        public DateTime CheckedOut { get; set; }

        public PaymentMethod Payment { get; set; }

    }
}
