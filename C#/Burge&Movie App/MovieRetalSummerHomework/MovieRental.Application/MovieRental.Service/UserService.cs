using Microsoft.EntityFrameworkCore;
using MovieRental.DataBase;
using MovieRental.DataBase.Interfaces;
using MovieRental.Domain;
using MovieRental.Dtos.Dto;
using MovieRental.Service.Interfaces;
using System.Security.Authentication;

namespace MovieRental.Service
{
    public class UserService : IUserService
    {
        private readonly MovieRentalDbContext _dbContext;
        private readonly IRepository<User> _userRepo;


        public UserService(MovieRentalDbContext dbContext, IRepository<User> userRepo)
        {
            _dbContext = dbContext;
            _userRepo = userRepo;
        }

        public User Login(string cardNumber, string email)
        {

            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                throw new ArgumentNullException("Please put cardnumber");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException("Please put email");
            }

            var user = _dbContext.User.FirstOrDefault(u => u.CardNumber == cardNumber && u.Email == email);

            if (user == null)
            {
                throw new AuthenticationException("The card number or email provided is incorrect.");
            }

            return user;
        }

        public User Register(User user)
        {
            User existingUser = _userRepo.GetById(user.Id);

            if (existingUser != null)
            {
                throw new Exception("User already exits");
            }

            _dbContext.Add(user);
            _dbContext.SaveChanges();

            return user;
        }

        public bool IsAccountValid(string cardNumber, string email)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                throw new ArgumentNullException(nameof(cardNumber), "Card number cannot be null or empty.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email), "Email cannot be null or empty.");
            }

            if (_dbContext.User == null)
            {
                throw new InvalidOperationException("UserRepository is not initialized.");
            }

            var userExists = _dbContext.User
                            .Any(u => u.CardNumber == cardNumber || u.Email == email);

            return userExists;
        }

        public User RegisterValidation(string cardNumber, string email, int age, string fullName)
        {
            if (IsAccountValid(cardNumber, email))
            {
                throw new InvalidOperationException("Sorry the user already exists");
            }

            if (string.IsNullOrEmpty(cardNumber))
            {
                throw new ArgumentNullException("Please fill Cardnumber");
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("Please fill Email");
            }

            if (string.IsNullOrEmpty(fullName))
            {
                throw new ArgumentNullException("Please fill Fullname");
            }

            //if (age < 18)
            //{
            //    throw new ArgumentException("You are under 18");
            //}

            return new User
            {
                CardNumber = cardNumber,
                Email = email,
                Age = age,
                FullName = fullName
            };
        }

        public UserDto GetCurrentUser(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
            {
                throw new ArgumentException("Card number cannot be null or empty.", nameof(cardNumber));
            }

            var user = _dbContext.User.FirstOrDefault(u => u.CardNumber == cardNumber);

            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Age = user.Age,
                CardNumber = user.CardNumber,
                Email = user.Email,
                CreatedOn = user.CreatedOn,
                IsSubscriptionExpired = user.IsSubscriptionExpired,
                SubscriptionType = user.SubscriptionType
            };
        }
    }
}
