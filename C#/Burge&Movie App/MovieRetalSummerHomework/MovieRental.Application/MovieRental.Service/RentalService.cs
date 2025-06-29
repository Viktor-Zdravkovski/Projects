using MovieRental.DataBase.Interfaces;
using MovieRental.Domain;
using MovieRental.Dtos.Dto;
using MovieRental.Service.Interfaces;

namespace MovieRental.Service
{
    public class RentalService : IRentalService
    {
        private readonly IRepository<Rental> _rentalRepository;

        public RentalService(IRepository<Rental> rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public void CreateRental(Rental rental)
        {
            //_rentalRepository.Add(rental);
            //_rentalRepository.SaveChanges();
            try
            {
                _rentalRepository.Add(rental);
                _rentalRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while creating the rental.", ex);
            }
        }
    }
}
