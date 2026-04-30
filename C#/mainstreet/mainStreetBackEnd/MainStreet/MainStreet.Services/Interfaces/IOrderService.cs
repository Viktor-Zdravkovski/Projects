using MainStreet.Dto.OrderDto;

namespace MainStreet.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrders();

        Task<OrderDto> GetOrderById(int id);

        Task<IEnumerable<OrderDto>> GetOrderByUserId(int userId, int count);

        Task<IEnumerable<OrderDto>> GetAllOrdersForCertainDay(DateTime date);

        Task AddOrder(AddOrderDto addOrderDto);

        Task UpdateOrder(int id, UpdateOrderDto updateOrderDto);

        Task DeleteOrder(int id);

    }
}
