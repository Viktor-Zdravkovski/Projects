using MainStreet.Domain.Enums;
using MainStreet.Domain.Models;

namespace MainStreet.Dto.OrderDto
{
    public class OrderDto
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public OrderStatus Status { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public decimal TotalPrice => OrderItems.Sum(x => x.SubTotal);
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
