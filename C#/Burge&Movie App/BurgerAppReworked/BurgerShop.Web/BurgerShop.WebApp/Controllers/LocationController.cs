using BurgerShop.DataBase;
using BurgerShop.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BurgerApp.WebApplication.Controllers
{
    public class LocationController : Controller
    {
        private readonly BurgerShopDbContext _db;

        public LocationController(BurgerShopDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Location> locationList = _db.Locations;
            return View(locationList);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Location obj)
        {
            if (ModelState.IsValid)
            {
                _db.Locations.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Location created successfully!";
                return RedirectToAction("Index", "Home");
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var locationFromDb = _db.Locations.Find(id);
            if (locationFromDb == null)
            {
                return NotFound();
            }
            return View(locationFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Location obj)
        {
            if (ModelState.IsValid)
            {
                _db.Locations.Update(obj);
                TempData["success"] = "Location edited successfully!";
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(obj);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var locationFromDb = _db.Locations.Find(id);
            if (locationFromDb == null)
            {
                return NotFound();
            }
            return View(locationFromDb);
        }
        [HttpPost]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Locations.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Locations.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Location deleted successfully!";
            return RedirectToAction("Index", "Home");
        }
    }
}

