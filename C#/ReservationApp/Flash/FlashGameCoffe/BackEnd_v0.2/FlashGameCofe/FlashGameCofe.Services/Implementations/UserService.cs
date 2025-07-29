using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FlashGameCofe.DataBase.Interfaces;
using FlashGameCofe.Domain.Enums;
using FlashGameCofe.Domain.Models;
using FlashGameCofe.Dto.ReservationsDto;
using FlashGameCofe.Dto.UserDtos;
using FlashGameCofe.Services.Interfaces;
using FlashGameCofe.Shared.Configuration;
using FlashGameCofe.Shared.CustomExceptions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using XSystem.Security.Cryptography;

namespace FlashGameCofe.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly FlashGameAppSettings _appSettings;

        public UserService(IUserRepository userRepository, IOptions<FlashGameAppSettings> options, IMapper mapper)
        {
            _userRepository = userRepository;
            _appSettings = options.Value;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUsersIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }

        public async Task<LogInResponseDto> LogInUser(LogInUserDto logInUserDto)
        {
            if (string.IsNullOrEmpty(logInUserDto.Email) || string.IsNullOrEmpty(logInUserDto.Password))
            {
                throw new NoDataException("Username & password are required fields!");
            }

            string hashPassword = HashPassword(logInUserDto.Password);
            User userDb = await _userRepository.LogInUser(logInUserDto.Email, hashPassword);

            if (userDb == null)
            {
                throw new NotFoundException("User was not found!");
            }

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_appSettings.SecretKey);

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
                UserId = userDb.Id,
                Token = token,
                Email = userDb.Email,
                Role = userDb.Role.ToString()
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
                Role = Roles.User
            };

            if (registerUserDto.Email == "blagojce@gmail.com")
            {
                user.Role = Roles.Admin;
            }

            await _userRepository.AddAsync(user);
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

            var userDb = await _userRepository.GetUserByEmail(registerUserDto.Email);
            if (userDb != null)
            {
                throw new NoDataException($"The user with username: {registerUserDto.Email} already exists");
            }
        }
    }
}
