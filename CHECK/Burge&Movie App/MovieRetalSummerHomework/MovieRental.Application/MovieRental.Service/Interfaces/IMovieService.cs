using MovieRental.Domain;
using MovieRental.Dtos.Dto;

namespace MovieRental.Service.Interfaces
{
    public interface IMovieService
    {
        List<MovieDto> GetAllMovies();

        MovieDto GetMovieById(int id);

        IEnumerable<Movie> IsOverCertainAge(int age);

        void UpdateMovieQuantity(int movieId, int newQuantity);

		void RentMovie(int movieId, string userName);

		IEnumerable<MovieDto> GetRentedMovies(string userName);
	}
}
