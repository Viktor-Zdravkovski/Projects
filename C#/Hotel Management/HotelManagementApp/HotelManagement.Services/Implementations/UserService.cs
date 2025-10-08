using AutoMapper;
using HotelManagement.DataBase.Interfaces;
using HotelManagement.Domain.Enums;
using HotelManagement.Domain.Models;
using HotelManagement.Dto.UsersDto;
using HotelManagement.Services.Interfaces;
using HotelManagement.Shared.Configuration;
using HotelManagement.Shared.CustomExceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XAct;
using XSystem.Security.Cryptography;

namespace HotelManagement.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly HotelManagementAppSettings _settings;

        public UserService(IUserRepository userRepository, IMapper mapper, HotelManagementAppSettings settings)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _settings = settings;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var user = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(user);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new NotFoundException("No user found.");

            return _mapper.Map<UserDto>(user);
        }


        public async Task<IEnumerable<UserDto>> GetUsersByRole(Roles role)
        {
            var users = await _userRepository.GetAllAsync();
            var usersByRoles = users.Where(x => x.Role == role);
            return _mapper.Map<IEnumerable<UserDto>>(usersByRoles);
        }

        public async Task UpdatePassword(int userId, string oldPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new NotFoundException("No user found.");

            if (string.IsNullOrEmpty(oldPassword))
                throw new Exception("Please fill this field");

            if (string.IsNullOrEmpty(newPassword))
                throw new Exception("Please fill this field");

            if (oldPassword == newPassword)
                throw new Exception("You can't put same password as an old one");

            var hashedOldPassword = HashPassword(oldPassword);

            if (hashedOldPassword != user.Password)
                throw new InvalidOperationException("The old password doesn't match.");

            var newUserPassowrd = HashPassword(newPassword);

            await _userRepository.UpdateAsync(user);
        }

        public async Task<LogInResponseDto> LogInUser(LogInUserDto logInUserDto)
        {
            if (string.IsNullOrEmpty(logInUserDto.Email) || string.IsNullOrEmpty(logInUserDto.Password))
                throw new NoDataException("This is requeired field");

            string hashPassword = HashPassword(logInUserDto.Password);
            User userDb = await _userRepository.LogInUser(logInUserDto.Email, hashPassword);

            if (userDb == null)
                throw new NoDataException("User not found");

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_settings.SecretKey);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, userDb.FirstName),
                        new Claim("userFullName", $"{userDb.FirstName} {userDb.LastName}"),
                        new Claim("UserId", userDb.Id.ToString()),
                        new Claim(ClaimTypes.Email, userDb.Email),
                        new Claim("Role", userDb.Role.ToString())
                    }
                )
            };

            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            string token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new LogInResponseDto
            {
                UserId = userDb.Id.ToString(),
                Token = token,
                Email = userDb.Email,
                Role = userDb.Role.ToString(),
            };
        }

        public async Task RegisterUser(RegisterUserDto registerUserDto)
        {
            await ValidateUser(registerUserDto);

            var hashPassword = HashPassword(registerUserDto.Password);

            User user = new User
            {
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                PhoneNumber = registerUserDto.PhoneNumber,
                Email = registerUserDto.Email,
                Password = hashPassword,
                PasswordConfirmed = hashPassword,
                Role = Roles.Customer,
            };

            if (registerUserDto.Email == "blagojce@gmail.com")
            {
                user.Role = Roles.Admin;
            }

            await _userRepository.AddAsync(user);
        }

        public Task DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        private string HashPassword(string password)
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);

            byte[] hashBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);

            string hashPassword = Encoding.ASCII.GetString(hashBytes);

            return hashPassword;
        }

        private async Task ValidateUser(RegisterUserDto registerUserDto)
        {
            if ((string.IsNullOrEmpty(registerUserDto.Email)) || (string.IsNullOrEmpty(registerUserDto.Password)))
            {
                throw new NoDataException("Email & Password are required fields!");
            }

            if (registerUserDto.Email.Length > 50)
            {
                throw new NoDataException("Username: Maximum length of the username is longer than 50 characters");
            }

            if ((string.IsNullOrEmpty(registerUserDto.FirstName)) || (string.IsNullOrEmpty(registerUserDto.LastName)))
            {
                throw new NoDataException("Firstname and Lastname are required fields");
            }

            if (registerUserDto.FirstName.Length > 30)
            {
                throw new NoDataException("Firstname: Firstname is longer than 30 characters");
            }

            if (registerUserDto.LastName.Length > 50)
            {
                throw new NoDataException("Lastname: Lastname is longer than 50 characters");
            }

            var userDb = await _userRepository.GetByEmailAsync(registerUserDto.Email);
            if (userDb != null)
            {
                throw new NoDataException($"The user with username: {registerUserDto.Email} already exists");
            }
        }


    }
}
