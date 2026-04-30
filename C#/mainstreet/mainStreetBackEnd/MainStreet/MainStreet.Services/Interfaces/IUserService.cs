using MainStreet.Dto.UserDto;

namespace MainStreet.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterUser(RegisterUserDto registerUserDto);

        Task<LogInResponseDto> LogInUser(LogInUserDto logInUserDto);

        Task<UserDto> GetUserByIdAsync(int id);
    }
}