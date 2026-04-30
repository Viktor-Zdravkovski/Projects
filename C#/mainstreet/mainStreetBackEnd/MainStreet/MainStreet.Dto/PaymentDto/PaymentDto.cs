using MainStreet.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MainStreet.Dto.PaymentDto
{
    public class PaymentDto
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Range(0.01, 10000.0 , ErrorMessage = "Price must be between 0.01 and 10,000")]
        public decimal Amount { get; set; }

        public DateTime PaidAt { get; set; } = DateTime.UtcNow;

        [Required]
        public PaymentMethod Method { get; set; }

        public string Status { get; set; } = "Pending";

        public string? TransactionId { get; set; }
    }
}
