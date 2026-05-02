using HotelManagement.Domain.Enums;

namespace HotelManagement.Domain.Models
{
    public class Reservation : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public DateTime CheckedIn { get; set; }
        public DateTime CheckedOut { get; set; }

        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
       
        public Payment Payment { get; set; }
    }
}
