using FlashGameCofe.Dto.UserDto;

namespace FlashGameCofe.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterUser(RegisterUserDto registerUserDto);

        Task<LogInResponseDto> LogInUser(LogInUserDto logInUserDto);
    }
}
