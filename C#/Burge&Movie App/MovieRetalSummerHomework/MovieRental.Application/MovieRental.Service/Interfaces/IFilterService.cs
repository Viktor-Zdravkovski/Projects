using MovieRental.Dtos.Dto;

namespace MovieRental.Service.Interfaces
{
    public interface IFilterService
    {
        //List<UserDto> GetAllUsers();

        RentalDto RentMovie(int movieId, int userId);

        //FilterDto GetFilterDetails();
    }
}
