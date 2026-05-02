using HotelManagement.Domain.Enums;

namespace HotelManagement.Dto.PaymentsDto
{
    public class AddPaymentDto
    {
        public int ReservationId { get; set; }

        public decimal Amount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
    }
}
