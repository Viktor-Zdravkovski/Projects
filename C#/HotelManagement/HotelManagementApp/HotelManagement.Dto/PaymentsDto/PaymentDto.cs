using HotelManagement.Domain.Enums;

namespace HotelManagement.Dto.PaymentsDto
{
    public class PaymentDto
    {
        public int Id { get; set; }

        public int ReservationId { get; set; }

        public decimal Amount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public PaymentStatus Status { get; set; }

        public DateTime PaidAt { get; set; }
    }
}
