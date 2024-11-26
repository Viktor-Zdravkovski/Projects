using BurgerShop.DataBase;
using BurgerShop.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BurgerApp.WebApplication.Controllers
{

    public class OrderController : Controller
    {
        private readonly BurgerShopDbContext _db;


        public OrderController(BurgerShopDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            var orders = _db.Orders.Include(o => o.Burgers).Include(o => o.Location).ToList();
            return View(orders);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var burgers = _db.Burgers
                         .Select(b => new { Value = b.Id, Text = b.Name })
                         .ToList();

            ViewBag.Burger = burgers;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Order obj)
        {
            if (ModelState.IsValid)
            {
                _db.Orders.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Order created successfully!";
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

            var orderFromDb = _db.Orders.Find(id);
            if (orderFromDb == null)
            {
                return NotFound();
            }

            var burgers = _db.Burgers
                        .Select(b => new { Value = b.Id, Text = b.Name })
                        .ToList();

            ViewBag.Burger = burgers;

            return View(orderFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Order obj)
        {
            if (ModelState.IsValid)
            {
                _db.Orders.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Order edited successfully!";

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

            var orderFromDb = _db.Orders.Find(id);

            if (orderFromDb == null)
            {
                return NotFound();
            }

            return View(orderFromDb);
        }

        [HttpPost]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Orders.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Orders.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Order deleted successfully!";

            return RedirectToAction("Index");
        }
    }
}