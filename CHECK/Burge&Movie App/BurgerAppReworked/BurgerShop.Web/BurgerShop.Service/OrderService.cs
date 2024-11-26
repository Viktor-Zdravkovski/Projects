using BurgerShop.DataBase.Interfaces;
using BurgerShop.Domain;
using BurgerShop.Dtos;
using BurgerShop.Service.Interfaces;

namespace BurgerShop.Service
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public List<OrderDto> GetAllOrders()
        {
            return _orderRepository.GetAll().Select(o => new OrderDto
            {
                Id = o.Id,
                FullName = o.FullName,
                Address = o.Address,
                IsDelivered = o.IsDelivered,
                BurgerIds = o.Burgers.Select(b => b.Id).ToList(),
                Location = o.Location
            }).ToList();
        }

        public OrderDto GetOrderById(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null) return null;
            return new OrderDto
            {
                Id = order.Id,
                FullName = order.FullName,
                Address = order.Address,
                IsDelivered = order.IsDelivered,
                BurgerIds = order.Burgers.Select(b => b.Id).ToList(),
                Location = order.Location
            };
        }

        public void AddOrder(OrderDto orderDto)
        {
            var order = new Order
            {
                FullName = orderDto.FullName,
                Address = orderDto.Address,
                IsDelivered = orderDto.IsDelivered,
                Location = orderDto.Location
            };

            _orderRepository.Add(order);
        }

        public void UpdateOrder(OrderDto orderDto)
        {
            var order = new Order
            {
                Id = orderDto.Id,
                FullName = orderDto.FullName,
                Address = orderDto.Address,
                IsDelivered = orderDto.IsDelivered,
                Location = orderDto.Location
            };

            _orderRepository.Update(order);
        }

        public void DeleteOrder(int id)
        {
            _orderRepository.Delete(id);
        }
    }
}
