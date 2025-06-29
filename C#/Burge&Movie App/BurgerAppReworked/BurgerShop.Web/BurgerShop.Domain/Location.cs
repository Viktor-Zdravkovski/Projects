namespace BurgerShop.Domain
{
    public class Location : BaseEntity
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime OpensAt { get; set; }

        public DateTime ClosesAt { get; set; }
    }
}
