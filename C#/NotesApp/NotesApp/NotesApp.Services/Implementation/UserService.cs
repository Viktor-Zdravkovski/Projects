using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NotesApp.DataAccess.Interfaces;
using NotesApp.Domain.Models;
using NotesApp.Dto.UserDto;
using NotesApp.Services.Interfaces;
using NotesApp.Shared.Configuration;
using NotesApp.Shared.CustomException;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace NotesApp.Services.Implementation
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private NotesAppSettings _settings;

        public UserService(IUserRepository userRepository, IOptions<NotesAppSettings> options)
        {
            _userRepository = userRepository;
            _settings = options.Value;
        }

        public string LoginUser(LoginUserDto loginUserDto)
        {
            if (string.IsNullOrEmpty(loginUserDto.Username) || string.IsNullOrEmpty(loginUserDto.Password))
            {
                throw new UserDataException("Username and password are required fields!");
            }

            string hashPassword = HashPassword(loginUserDto.Password);

            User userdb = _userRepository.LoginUser(loginUserDto.Username, hashPassword);
            if (userdb == null)
            {
                throw new UserNotFoundException("User not found!!!");
            }

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_settings.SecretKey);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(
                        new[]
                        {
                            new Claim(ClaimTypes.Name, userdb.Username),
                            new Claim("userFullName", $"{userdb.FirstName} {userdb.LastName}"),
                            new Claim("userId", userdb.Id.ToString())
                            //new Claim(ClaimTypes.Role, userdb.Role)
                        }
                    )
            };

            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }

        public void RegisterUser(RegisterUserDto registerUserDto)
        {
            ValidateUser(registerUserDto);

            var hashPassword = HashPassword(registerUserDto.Password);

            User user = new User
            {
                Username = registerUserDto.Username,
                Password = hashPassword,
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName
            };

            _userRepository.Add(user);
        }

        private string HashPassword(string password)
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);

            byte[] hashBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);

            string hashPassword = Encoding.ASCII.GetString(hashBytes);

            return hashPassword;
        }

        private void ValidateUser(RegisterUserDto registerUserDto)
        {
            if (string.IsNullOrEmpty(registerUserDto.Username) || string.IsNullOrEmpty(registerUserDto.Password))
            {
                throw new UserDataException("Username and password are required fields!");
            }

            if (registerUserDto.Username.Length > 50)
            {
                throw new UserDataException("Username: Maximum length for username is 50 characters!!!");
            }

            if (string.IsNullOrEmpty(registerUserDto.FirstName) || string.IsNullOrEmpty(registerUserDto.LastName))
            {
                throw new UserDataException("FirstName and LastName are required fields!");
            }

            if (registerUserDto.FirstName.Length > 100 || registerUserDto.LastName.Length > 100)
            {
                throw new UserDataException("FirstName and LastName: Maximum length is 100 characters!!!");
            }

            if (registerUserDto.Password != registerUserDto.ConfirmedPassword)
            {
                throw new UserDataException("Password must match!!!");
            }

            var userDb = _userRepository.GetUserByUsername(registerUserDto.Username);
            if (userDb != null)
            {
                throw new UserDataException($"The username {registerUserDto.Username} already exists!!");
            }
        }

    }
}
