using HotelManagement.Domain.Enums;
using HotelManagement.Domain.Models;

namespace HotelManagement.Dto.RoomsDto
{
    public class RoomDto
    {
        public string RoomNumber { get; set; }

        public string Type { get; set; } // dbl bed, single bed

        public decimal PricePerNight { get; set; }

        public RoomStatus Status { get; set; }
    }
}
