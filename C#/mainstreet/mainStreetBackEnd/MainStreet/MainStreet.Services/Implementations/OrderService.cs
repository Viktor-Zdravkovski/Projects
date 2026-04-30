using AutoMapper;
using MainStreet.DataBase.Interfaces;
using MainStreet.Domain.Models;
using MainStreet.Dto.OrderDto;
using MainStreet.Dto.OrderItemDto;
using MainStreet.Services.Interfaces;
using MainStreet.Shared.CustomExceptions;

namespace MainStreet.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrders()
        {
            var orders = await _orderRepository.GetActiveOrdersAsync();

            if (orders.Any())
            {
                return new List<OrderDto>();
            }

            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetOrderById(int id)
        {
            var order = await _orderRepository.GetOrderWithDetailsAsync(id);
            if (order == null)
                return null;

            var orderDto = _mapper.Map<OrderDto>(order);

            orderDto.CustomerName = $"{order.User.FirstName} {order.User.LastName}";

            return orderDto;

        }

        public async Task<IEnumerable<OrderDto>> GetOrderByUserId(int userId, int count)
        {
            IEnumerable<Order> order = await _orderRepository.GetRecentOrdersByUserIdAsync(userId, count);

            if (order == null)
                return Enumerable.Empty<OrderDto>();

            IEnumerable<OrderDto> orderDto = _mapper.Map<IEnumerable<OrderDto>>(order);

            foreach (var dto in orderDto)
            {
                var currentSource = order.First(x => x.Id == dto.Id);
                dto.CustomerName = $"{currentSource.User.FirstName} {currentSource.User.LastName}";
            }

            return orderDto;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersForCertainDay(DateTime date)
        {
            var orders = await _orderRepository.GetOrdersByCertainDateAsync(date);

            if (orders == null)
                return Enumerable.Empty<OrderDto>();

            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

            return orderDtos;
        }

        public async Task AddOrder(AddOrderDto addOrderDto)
        {
            var order = _mapper.Map<Order>(addOrderDto);
            await _orderRepository.AddAsync(order);
        }

        public async Task UpdateOrder(int id, UpdateOrderDto updateOrderDto)
        {
            var existingOrder = await _orderRepository.GetIdByAsync(id);

            if (existingOrder == null)
            {
                throw new NotFoundException("No order found to update");
            }

            await _orderRepository.UpdateAsync(existingOrder);
        }

        public async Task DeleteOrder(int id)
        {
            var order = await _orderRepository.GetIdByAsync(id);

            if (order == null)
            {
                throw new NotFoundException($"Order with ID:{id} was not found");
            }

            await _orderRepository.DeleteAsync(order);
        }
    }
}
