using BurgerShop.DataBase;
using BurgerShop.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BurgerApp.WebApplication.Controllers
{
    public class BurgerController : Controller
    {
        private readonly BurgerShopDbContext _db;

        public BurgerController(BurgerShopDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Burger> objBurgerList = _db.Burgers;
            return View(objBurgerList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Burger obj)
        {
            if (ModelState.IsValid)
            {
                _db.Burgers.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Burger created successfully!";
                return RedirectToAction("Index");
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
            var burgerFromDb = _db.Burgers.Find(id);
            if (burgerFromDb == null)
            {
                return NotFound();
            }
            return View(burgerFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Burger obj)
        {
            if (ModelState.IsValid)
            {
                _db.Burgers.Update(obj);
                TempData["success"] = "Burger edited successfully!";
                _db.SaveChanges();
                return RedirectToAction("Index");
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
            var burgerFromDb = _db.Burgers.Find(id);
            if (burgerFromDb == null)
            {
                return NotFound();
            }
            return View(burgerFromDb);
        }
        [HttpPost]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Burgers.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Burgers.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Burger deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
