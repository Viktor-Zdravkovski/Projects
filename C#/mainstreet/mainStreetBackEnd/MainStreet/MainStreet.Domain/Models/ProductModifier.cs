namespace MainStreet.Domain.Models
{
    public class ProductModifier : BaseEntity
    {
        public int ProductId { get; set; }

        public int ModifierId { get; set; }

    }
}
