using Microsoft.AspNetCore.Mvc;
using MovieRental.DataBase;
using MovieRental.DataBase.Interfaces;
using MovieRental.Domain;
using MovieRental.Dtos.Dto;
using MovieRental.Service.Interfaces;

namespace MovieRental.WebApp.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieRentalDbContext _context;
        private readonly IMovieService _movieService;
        private readonly IRepository<Movie> _movieRepository;

        public MovieController(IMovieService movieService, MovieRentalDbContext context, IRepository<Movie> movieRepo)
        {
            _movieService = movieService;
            _movieRepository = movieRepo;
            _context = context;
        }

        [HttpGet("allMovies")]
        public IActionResult GetAll()
        {
            var movies = _movieService.GetAllMovies();
            return View(movies);
        }

        [HttpGet("details/{id}")] // ako ne raboti izbrishi samo /{id}
        public IActionResult GetMovieDetails(int id)
        {
            //var moviebyId = _movieService.GetMovieById(id);
            //return View(moviebyId);

            var movieById = _movieService.GetMovieById(id);
            if (movieById == null)
            {
                return NotFound();
            }
            return View(movieById);
        }

        //[HttpPost("RentMovie")]
        //public IActionResult RentMovie(int id)
        //{
        //    //try
        //    //{
        //    //    _movieService.RentMovie(id, User.Identity.Name);
        //    //    return RedirectToAction("RentedMovies", "Movie");
        //    //}
        //    //catch (InvalidOperationException ex)
        //    //{
        //    //    TempData["ErrorMessage"] = ex.Message;
        //    //    return RedirectToAction("Index", "Home");
        //    //}

        //    try
        //    {
        //        _movieService.RentMovie(id, User.Identity.Name);
        //        return RedirectToAction("RentedMovies");
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        TempData["ErrorMessage"] = ex.Message;
        //        return RedirectToAction("GetAll");
        //    }
        //}

        [HttpPost("RentMovie")]
        public IActionResult RentMovie(int id)
        {
            try
            {
                _movieService.RentMovie(id, User.Identity.Name);
                return RedirectToAction("RentedMovies");
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("GetAll"); 
            }
        }

        //public IActionResult RentedMovies()
        //{
        //    //var rentedMovies = _movieService.GetRentedMovies(User.Identity.Name);
        //    //return View(rentedMovies);

        //    var rentedMovies = _movieService.GetRentedMovies(User.Identity.Name);
        //    return View(rentedMovies);
        //}
        public IActionResult RentedMovies()
        {
            var rentedMovies = _movieService.GetRentedMovies(User.Identity.Name);
            return View(rentedMovies);
        }

    }
}
