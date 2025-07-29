using FlashGameCofe.Dto.UserDtos;

namespace FlashGameCofe.Dto.ReservationsDto
{
    public class ReservationDto
    {
        public int Id { get; set; }

        public string? NoteDescription { get; set; }

        public DateTime StartingTime { get; set; }

        public int UserId { get; set; }

        //public string PhoneNumber { get; set; }

        public UserDto User { get; set; }
    }
}
