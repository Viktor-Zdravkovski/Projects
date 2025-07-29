using AutoMapper;
using FlashGameCofe.Domain.Models;
using FlashGameCofe.Dto.ReservationsDto;
using FlashGameCofe.Dto.UserDtos;

namespace FlashGameCofe.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Reservation, ReservationDto>().ReverseMap();
            CreateMap<AddReservationDto, Reservation>();
            CreateMap<UpdateReservationDto, Reservation>();

            CreateMap<Reservation, ReservationDto>()
            .ForMember(dest => dest.StartingTime,
                       opt => opt.MapFrom(src => src.StartingTime.ToLocalTime()));

            CreateMap<User, UserDto>();
        }
    }
}
