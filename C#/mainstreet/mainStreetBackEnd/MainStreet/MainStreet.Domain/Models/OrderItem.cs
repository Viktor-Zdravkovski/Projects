using System;
using System.Collections.Generic;
using System.Text;

namespace MainStreet.Domain.Models
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal PriceAtPurchase { get; set; }

        public virtual ICollection<OrderItemModifier> SelectedModifiers { get; set; } = new List<OrderItemModifier>();

        public string SpecialInstructions { get; set; }

        public bool IsOnHold { get; set; }

        public decimal SubTotal => (PriceAtPurchase + SelectedModifiers.Sum(m => m.Modifier.PriceModifier)) * Quantity;
    }
}
