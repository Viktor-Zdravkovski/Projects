using MovieRental.Domain;
using MovieRental.Dtos.Dto;

namespace MovieRental.Mapper
{
    public static class MapperExtensions
    {
        public static MovieDto Map(this Movie movie)
        {
            return new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Genre = movie.Genre,
                IsAvailable = movie.IsAvailable,
                AgeRestriction = movie.AgeRestriction,
                Length = movie.Length,
                Language = movie.Language,
                Quantity = movie.Quantity,
                ReleaseDate = movie.ReleaseDate
            };
        }

        public static RentalDto Map(this Rental rent)
        {
            return new RentalDto
            {
                Id = rent.Id,
                MovieId = rent.MovieId,
                UserId = rent.UserId,
                RentedOn = rent.RentedOn,
                ReturnedOn = rent.ReturnedOn.HasValue ? rent.ReturnedOn.Value : (DateTime?)null
            };
        }

        public static UserDto Map(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Age = user.Age,
                CardNumber = user.CardNumber,
                Email = user.Email
            };
        }
    }
}