using BurgerShop.DataBase;
using BurgerShop.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BurgerApp.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly BurgerShopDbContext _db;

        public HomeController(BurgerShopDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            var locations = _db.Locations.ToList();
            return View(locations);
            //return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
