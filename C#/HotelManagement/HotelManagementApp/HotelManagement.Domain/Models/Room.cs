using HotelManagement.Domain.Enums;

namespace HotelManagement.Domain.Models
{
    public class Room : BaseEntity
    {
        public string RoomNumber { get; set; }

        public string Type { get; set; } // dbl bed, single bed

        public decimal PricePerNight { get; set; }

        public RoomStatus Status { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
