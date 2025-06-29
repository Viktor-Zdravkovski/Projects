using MovieRental.Domain;
using MovieRental.Dtos.Dto;

namespace MovieRental.Service.Interfaces
{
    public interface IRentalService
    {
        void CreateRental(Rental rental);
    }
}
