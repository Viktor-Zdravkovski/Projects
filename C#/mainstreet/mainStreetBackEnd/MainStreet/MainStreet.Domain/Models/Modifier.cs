namespace MainStreet.Domain.Models
{
    public class Modifier : BaseEntity
    {
        public string Name { get; set; }

        public decimal PriceModifier { get; set; }
    }
}
