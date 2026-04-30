using MainStreet.Domain.Enums;
using MainStreet.Domain.Models;

namespace MainStreet.Dto.OrderDto
{
    public class UpdateOrderDto
    {
        public string CustomerName { get; set; }

        public OrderStatus Status { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
