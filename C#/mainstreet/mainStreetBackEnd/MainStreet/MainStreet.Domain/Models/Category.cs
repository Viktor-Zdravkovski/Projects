namespace MainStreet.Domain.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
