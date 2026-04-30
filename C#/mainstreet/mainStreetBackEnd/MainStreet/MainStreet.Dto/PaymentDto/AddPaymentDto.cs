using MainStreet.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MainStreet.Dto.PaymentDto
{
    public class AddPaymentDto
    {
        [Required]
        public int OrderId { get; set; }

        [Range(0.01, 10000.0)]
        public decimal Amount { get; set; }

        public PaymentMethod Method { get; set; }

        public string? TransactionNote { get; set; }
    }
}
