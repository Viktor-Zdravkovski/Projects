using AutoMapper;
using MainStreet.Domain.Models;
using MainStreet.Dto.OrderDto;
using MainStreet.Dto.OrderItemDto;
using MainStreet.Dto.PaymentDto;
using MainStreet.Dto.UserDto;

namespace MainStreet.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserDto, User>();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.CustomerName,
                 opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));

            CreateMap<AddOrderDto, Order>();
            CreateMap<UpdateOrderDto, Order>();

            CreateMap<AddOrderItemDto, OrderItem>()
                .ForMember(x => x.PriceAtPurchase, opt => opt.Ignore());

            CreateMap<OrderItem, AddOrderItemDto>();

            CreateMap<AddPaymentDto, Payment>();
            CreateMap<Payment, PaymentDto>();
        }
    }
}
