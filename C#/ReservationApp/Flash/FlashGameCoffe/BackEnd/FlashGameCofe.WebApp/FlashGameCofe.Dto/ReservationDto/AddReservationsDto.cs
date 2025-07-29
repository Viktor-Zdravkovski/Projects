using FlashGameCofe.Dto.NoteDto;
using System.ComponentModel.DataAnnotations;

namespace FlashGameCofe.Dto.ReservationDto
{
    public class AddReservationsDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public NotesDto? Note { get; set; }

        [Required]
        public DateTime StartingHour { get; set; }
    }
}
