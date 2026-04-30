namespace MainStreet.Domain.Models
{
    public class OrderItemModifier
    {
        public int OrderItemId { get; set; }

        public int ModifierId { get; set; }

        public virtual Modifier Modifier { get; set; }
    }
}
