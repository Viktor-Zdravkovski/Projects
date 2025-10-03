using HotelManagement.Domain.Enums;
using System.Reflection.Metadata;

namespace HotelManagement.Domain.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public Roles Role { get; set; }

        public ICollection<Reservation> Reservations { get; set; }


    }
}
