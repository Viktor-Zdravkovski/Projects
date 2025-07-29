using AutoMapper;
using FlashGameCofe.Domain.Models;
using FlashGameCofe.Dto.NoteDto;
using FlashGameCofe.Dto.ReservationDto;

namespace FlashGameCofe.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Reservation, ReservationsDto>().ReverseMap();
            CreateMap<AddReservationsDto, Reservation>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => new User
                {
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    PhoneNumber = src.PhoneNumber,
                    Email = src.Email,
                    Role = Domain.Enums.Roles.User
                }))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
                .ForMember(dest => dest.StartingHour, opt => opt.MapFrom(src => src.StartingHour));

            CreateMap<Note, NotesDto>().ReverseMap();
            CreateMap<AddNotesDto, Note>();
            CreateMap<UpdateNotesDto, Note>();
        }
    }
}
