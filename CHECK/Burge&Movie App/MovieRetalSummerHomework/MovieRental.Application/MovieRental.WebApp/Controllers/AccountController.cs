using Microsoft.AspNetCore.Mvc;
using MovieRental.DataBase;
using MovieRental.Domain;
using MovieRental.Service.Interfaces;

namespace MovieRental.WebApp.Controllers
{
    [Route("")]
    public class AccountController : Controller
    {
        private readonly MovieRentalDbContext _db;
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login(string cardNumber, string email)
        {
            bool isValid = _userService.IsAccountValid(cardNumber, email);

            if (isValid)
            {
                HttpContext.Session.SetString("CurrentCardNumber", cardNumber);
                HttpContext.Session.SetString("IsAuthenticated", "true");
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Invalid card number or email";
            return View();
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            var validatedUser = _userService.RegisterValidation(user.CardNumber, user.Email, user.Age, user.FullName);
            //_userService.Register(validatedUser);

            if (validatedUser != null)
            {
                HttpContext.Session.SetString("IsAuthenticated", "true");
                _userService.Register(validatedUser);
                return RedirectToAction("Index", "Home");
            }

            return View();

        }
    }
}
