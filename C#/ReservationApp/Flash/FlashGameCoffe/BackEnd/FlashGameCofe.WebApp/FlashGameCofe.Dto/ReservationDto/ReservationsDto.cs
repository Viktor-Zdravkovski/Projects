using System.ComponentModel.DataAnnotations;
using FlashGameCofe.Dto.NoteDto;

namespace FlashGameCofe.Dto.ReservationDto
{
    public class ReservationsDto
    {
        public int Id { get; set; }

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
        public DateTime StartingTime { get; set; }
    }
}
