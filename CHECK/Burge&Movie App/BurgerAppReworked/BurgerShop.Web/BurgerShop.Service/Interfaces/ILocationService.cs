using BurgerShop.Domain;
using BurgerShop.Dtos;

namespace BurgerShop.Service.Interfaces
{
    public interface ILocationService
    {
        List<LocationDto> GetAllLocations();

        LocationDto GetLocationById(int id);

        void AddLocation(LocationDto location);

        void UpdateLocation(LocationDto location);

        void DeleteLocation(int id);
    }
}
