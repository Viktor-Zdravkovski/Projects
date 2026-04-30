using MainStreet.Domain.Enums;

namespace MainStreet.Domain.Models
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public string CustomerName { get; set; }

        //public OrderSource Source { get; set; }

        public OrderStatus Status { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public decimal TotalPrice => OrderItems.Sum(x => x.SubTotal);
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
