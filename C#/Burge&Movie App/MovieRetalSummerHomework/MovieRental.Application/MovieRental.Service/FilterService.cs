using MovieRental.DataBase.EFImplementations;
using MovieRental.DataBase.Interfaces;
using MovieRental.Domain;
using MovieRental.Dtos.Dto;
using MovieRental.Mapper;
using MovieRental.Service.Interfaces;

namespace MovieRental.Service
{
    public class FilterService : IFilterService
    {
        private readonly IRepository<Movie> _movieRepository;
        private readonly IRepository<Rental> _rentalRepository;
        private readonly IRepository<User> _userRepository;

        public FilterService(IRepository<Movie> movies, IRepository<Rental> rentalRepository, IRepository<User> userRepository)
        {
            _movieRepository = movies;
            _rentalRepository = rentalRepository;
            _userRepository = userRepository;
        }

        public List<MovieDto> GetAllMovies()
        {
            return _movieRepository.GetAll().Select(x => x.Map()).ToList();
        }

        public RentalDto RentMovie(int movieId, int userId)
        {
            var movie = _movieRepository.GetById(movieId);
            var user = _userRepository.GetById(userId);

            if (movie == null)
            {
                throw new ArgumentException("Movie not found");
            }

            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            //if (!movie.IsAvailable)
            //{
            //	throw new InvalidOperationException("Movie is not available");
            //}

            //if (movie.Quantity <= 0)
            //{
            //	movie.IsAvailable = false;
            //	throw new InvalidOperationException("Movie is out of stock");
            //}

            var rental = new Rental
            {
                //UserId = userId,
                //MovieId = movieId,
                //RentedOn = DateTime.UtcNow,
                Id = movieId,
                MovieId = movie.Id,
                UserId = userId,
            };

            _rentalRepository.Add(rental);
            _rentalRepository.SaveChanges();

            movie.Quantity--;

            if (movie.Quantity <= 0)
            {
                movie.IsAvailable = false;
            }

            _movieRepository.Update(movie);
            _movieRepository.SaveChanges();

            var rentalDto = new RentalDto
            {
                Id = rental.Id,
                UserId = rental.UserId,
                MovieId = rental.MovieId,
                ReturnedOn = rental.ReturnedOn,
            };

            return rentalDto;
        }

        //public FilterDto GetFilterDetails()
        //{
        //    return new FilterDto
        //    {
        //        Movie = GetAllMovies()
        //    };
        //}

        //public List<UserDto> GetAllUsers()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
