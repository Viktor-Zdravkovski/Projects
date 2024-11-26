namespace BurgerShop.Domain
{
    public class Order : BaseEntity
    {
        public string FullName { get; set; }

        public string Address { get; set; }

        public bool IsDelivered { get; set; }

        public List<Burger> Burgers { get; set; } = new List<Burger>();

        public int LocationId { get; set; }

        public Location Location { get; set; }
    }
}
