using MainStreet.Domain.Enums;

namespace MainStreet.Domain.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        //public DateTime BirthDate { get; set; }

        public Roles Role { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public int OrderId { get; set; }
    }
}
