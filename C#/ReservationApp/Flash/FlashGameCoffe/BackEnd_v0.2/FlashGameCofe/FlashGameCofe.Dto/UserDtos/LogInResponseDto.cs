namespace FlashGameCofe.Dto.UserDtos
{
    public class LogInResponseDto
    {
        public string Token { get; set; }

        public string Email { get; set; }

        public int UserId { get; set; }

        public string Role { get; set; }
    }
}
