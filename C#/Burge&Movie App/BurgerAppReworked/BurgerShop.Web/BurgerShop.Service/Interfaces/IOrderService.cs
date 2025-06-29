using BurgerShop.Domain;
using BurgerShop.Dtos;

namespace BurgerShop.Service.Interfaces
{
    public interface IOrderService
    {
        List<OrderDto> GetAllOrders();

        OrderDto GetOrderById(int id);

        void AddOrder(OrderDto order);

        void UpdateOrder(OrderDto order);

        void DeleteOrder(int id);
    }
}
