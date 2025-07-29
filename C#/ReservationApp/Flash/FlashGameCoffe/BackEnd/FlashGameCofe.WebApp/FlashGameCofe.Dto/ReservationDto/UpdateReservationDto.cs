using FlashGameCofe.Dto.NoteDto;
using System.ComponentModel.DataAnnotations;

namespace FlashGameCofe.Dto.ReservationDto
{
    public class UpdateReservationDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public NotesDto? Note { get; set; }

        public DateTime StartingTime { get; set; }
    }
}
