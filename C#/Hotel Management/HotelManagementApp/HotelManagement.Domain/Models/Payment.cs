using HotelManagement.Domain.Enums;

namespace HotelManagement.Domain.Models
{
    public class Payment : BaseEntity
    {
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public DateTime PaidAt { get; set; }
    }
}
