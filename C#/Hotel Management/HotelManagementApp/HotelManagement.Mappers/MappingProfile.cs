using AutoMapper;
using HotelManagement.Domain.Models;
using HotelManagement.Dto.PaymentsDto;
using HotelManagement.Dto.ReservationsDto;
using HotelManagement.Dto.RoomsDto;
using HotelManagement.Dto.UsersDto;

namespace HotelManagement.Mappers
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            // Reservations mapper
            CreateMap<Reservation, ReservationDto>().ReverseMap();
            CreateMap<AddReservationDto, Reservation>();
            CreateMap<UpdateReservationDto, Reservation>();

            // Rooms mapper
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<AddRoomDto, RoomDto>();
            CreateMap<UpdateRoomDto, RoomDto>();

            // Payment mapper
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<AddPaymentDto, Payment>();

            // User mapper
            CreateMap<User, UserDto>();
        }
    }
}
