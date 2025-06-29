using BurgerShop.Domain;
using BurgerShop.Dtos;

namespace BurgerShop.Service.Interfaces
{
    public interface IBurgerService
    {
        List<BurgerDto> ShowAllBurgers();

        BurgerDto GetBurgerById(int id);

        void AddBurger(BurgerDto burger);

        void EditBurger(BurgerDto burger);

        void DeleteBurger(int id);
    }
}
