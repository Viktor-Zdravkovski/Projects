namespace MainStreet.Domain.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public decimal BasePrice { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<ProductModifier> AllowedModifiers { get; set; }
    }
}
