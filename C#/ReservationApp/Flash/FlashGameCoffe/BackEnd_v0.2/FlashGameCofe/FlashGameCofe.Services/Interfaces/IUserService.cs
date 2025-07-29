using FlashGameCofe.Domain.Models;
using FlashGameCofe.Dto.UserDtos;

namespace FlashGameCofe.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterUser(RegisterUserDto registerUserDto);

        Task<LogInResponseDto> LogInUser(LogInUserDto logInUserDto);

        Task<UserDto> GetUsersIdAsync(int id);

    }
}
