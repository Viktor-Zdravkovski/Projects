using HotelManagement.Domain.Enums;

namespace HotelManagement.Dto.RoomsDto
{
    public class UpdateRoomDto
    {
        public string Type { get; set; } 

        public decimal PricePerNight { get; set; }

        public RoomStatus Status { get; set; }
    }
}
