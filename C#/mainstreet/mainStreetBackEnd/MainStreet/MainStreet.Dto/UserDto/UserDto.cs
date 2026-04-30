using MainStreet.Domain.Enums;

namespace MainStreet.Dto.UserDto
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Roles Role { get; set; }
    }
}
