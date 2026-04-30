using MainStreet.Domain.Enums;

namespace MainStreet.Domain.Models
{
    public class Payment : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public decimal Amount { get; set; }
        public DateTime PaidAt { get; set; } = DateTime.UtcNow;

        public PaymentMethod Method { get; set; }

        public string TransactionId { get; set; }
    }
}
