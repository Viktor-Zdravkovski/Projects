using HotelManagement.Domain.Enums;
using HotelManagement.Dto.UsersDto;

namespace HotelManagement.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();

        Task<IEnumerable<UserDto>> GetUsersByRole(Roles role);

        Task UpdatePassword(int userId, string oldPassword, string newPassword);

        Task RegisterUser(RegisterUserDto registerUserDto);

        Task<LogInResponseDto> LogInUser(LogInUserDto logInUserDto);

        Task<UserDto> GetUserByIdAsync(int id);

        Task DeleteUser(int userId);
    }
}
