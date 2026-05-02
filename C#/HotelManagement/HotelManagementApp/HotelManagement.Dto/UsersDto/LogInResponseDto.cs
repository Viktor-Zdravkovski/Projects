namespace HotelManagement.Dto.UsersDto
{
    public class LogInResponseDto
    {
        public string Token { get; set; }

        public string Email { get; set; }

        public string UserId { get; set; }

        public string Role { get; set; }
    }
}
