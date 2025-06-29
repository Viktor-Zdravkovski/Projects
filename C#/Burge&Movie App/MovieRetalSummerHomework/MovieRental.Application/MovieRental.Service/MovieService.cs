using MovieRental.DataBase;
using MovieRental.DataBase.EFImplementations;
using MovieRental.DataBase.Interfaces;
using MovieRental.Domain;
using MovieRental.Domain.Enums;
using MovieRental.Dtos.Dto;
using MovieRental.Mapper;
using MovieRental.Service.Interfaces;

namespace MovieRental.Service
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Movie> _movieRepository;
        private readonly MovieRentalDbContext _dbContext;
        public MovieService(IRepository<Movie> movieRepository, IRepository<User> userRepository)
        {
            _movieRepository = movieRepository;
            _userRepository = userRepository;
        }

        public List<MovieDto> GetAllMovies()
        {
            //return _movieRepository.GetAll().Select(x => new MovieDto
            //{
            //    Id = x.Id,
            //    Title = x.Title,
            //    Genre = x.Genre,
            //    Language = x.Language,
            //    IsAvailable = x.IsAvailable,
            //    ReleaseDate = x.ReleaseDate,
            //    Length = x.Length,
            //    AgeRestriction = x.AgeRestriction,
            //    Quantity = x.Quantity
            //}).ToList();

            return _movieRepository.GetAll()
                                   .Select(x => x.Map())
                                   .ToList();
        }

        public MovieDto GetMovieById(int id)
        {
            var movie = _movieRepository.GetById(id);
            if (movie == null) return null;

            //return new MovieDto
            //{
            //    Id = movie.Id,
            //    Title = movie.Title,
            //    Genre = movie.Genre,
            //    Language = movie.Language,
            //    IsAvailable = movie.IsAvailable,
            //    ReleaseDate = movie.ReleaseDate,
            //    Length = movie.Length,
            //    AgeRestriction = movie.AgeRestriction,
            //    Quantity = movie.Quantity
            //};
            return movie.Map();
        }

        public IEnumerable<MovieDto> GetRentedMovies(string userName)
        {
            var rentedMovies = _movieRepository.GetAll()
                .Where(m => !m.IsAvailable)
                .Select(m => new MovieDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Genre = m.Genre,
                    Language = m.Language,
                    IsAvailable = m.IsAvailable,
                    ReleaseDate = m.ReleaseDate,
                    Length = m.Length,
                    AgeRestriction = m.AgeRestriction,
                    Quantity = m.Quantity
                });

            return rentedMovies;
        }

        public IEnumerable<Movie> IsOverCertainAge(int age)
        {
            var allMovies = _movieRepository.GetAll();

            //var filteredMovies = allMovies.Where(m => m.AgeRestriction < 18)

            if (age < 18)
            {

                return allMovies.Where(m => m.AgeRestriction < 18);
            }

            return allMovies;
        }

        public void RentMovie(int movieId, string userName)
        {
            //var movie = _movieRepository.GetById(movieId);
            //if (movie == null)
            //{
            //	throw new InvalidOperationException("Movie not found.");
            //}

            //if (!movie.IsAvailable)
            //{
            //	throw new InvalidOperationException("Movie is not available.");
            //}

            //// Mark the movie as rented
            //movie.IsAvailable = false;
            //// You might also want to update the quantity if applicable
            //// movie.Quantity -= 1;

            //// Assuming you are storing who rented the movie in a different way
            //// (e.g., in a rental history or separate table)

            //_movieRepository.Update(movie);

            var movie = _movieRepository.GetById(movieId);
            if (movie == null)
            {
                throw new InvalidOperationException("Movie not found.");
            }

            if (!movie.IsAvailable)
            {
                throw new InvalidOperationException("Movie is not available.");
            }

            movie.IsAvailable = false;
            movie.Quantity -= 1;

            _movieRepository.Update(movie);
        }

        public void UpdateMovieQuantity(int movieId, int newQuantity)
        {
            //var movie = _movieRepository.GetById(movieId);

            //if (movie != null)
            //{
            //    movie.Quantity = newQuantity;
            //    _movieRepository.Update(movie);
            //    _movieRepository.SaveChanges();
            //}
            try
            {
                var movie = _movieRepository.GetById(movieId);
                if (movie != null)
                {
                    movie.Quantity = newQuantity;
                    _movieRepository.Update(movie);
                    _movieRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the movie quantity.", ex);
            }
        }
    }
}
