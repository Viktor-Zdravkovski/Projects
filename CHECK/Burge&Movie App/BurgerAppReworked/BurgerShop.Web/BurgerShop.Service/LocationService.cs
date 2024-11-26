using BurgerShop.DataBase.Interfaces;
using BurgerShop.Domain;
using BurgerShop.Dtos;
using BurgerShop.Service.Interfaces;

namespace BurgerShop.Service
{
    public class LocationService : ILocationService
    {
        private readonly IRepository<Location> _locationRepository;

        public LocationService(IRepository<Location> locationRepository)
        {
            _locationRepository = locationRepository;

        }

        public List<LocationDto> GetAllLocations()
        {
            return _locationRepository.GetAll().Select(b => new LocationDto
            {
                Id = b.Id,
                LocationName = b.Name,
                Address = b.Address,
                OpensAt = b.OpensAt,
                ClosesAt = b.ClosesAt,
            }).ToList();
        }

        public LocationDto GetLocationById(int id)
        {
            var location = _locationRepository.GetById(id);
            if (location == null) return null;

            return new LocationDto
            {
                Id = location.Id,
                LocationName = location.Name,
                Address = location.Address,
                OpensAt = location.OpensAt,
                ClosesAt = location.ClosesAt
            };
        }

        public void AddLocation(LocationDto locationDto)
        {
            var location = new Location
            {
                Id = locationDto.Id,
                Name = locationDto.LocationName,
                Address = locationDto.Address,
                OpensAt = locationDto.OpensAt,
                ClosesAt = locationDto.ClosesAt,
            };

            _locationRepository.Add(location);
        }

        public void UpdateLocation(LocationDto locationDto)
        {
            var location = new Location
            {
                Id = locationDto.Id,
                Name = locationDto.LocationName,
                Address = locationDto.Address,
                OpensAt = locationDto.OpensAt,
                ClosesAt = locationDto.ClosesAt,
            };

            _locationRepository.Update(location);
        }

        public void DeleteLocation(int id)
        {
            _locationRepository.Delete(id);

        }
    }
}
