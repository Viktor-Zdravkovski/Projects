using BurgerShop.Domain;

namespace BurgerShop.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Address { get; set; }

        public bool IsDelivered { get; set; }

        public List<Burger> Burgers { get; set; }

        public Location Location { get; set; }

        public List<int> BurgerIds { get; set; }

    }
}
