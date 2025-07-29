using System.ComponentModel.DataAnnotations;

namespace FlashGameCofe.Dto.NoteDto
{
    public class AddNotesDto
    {
        [Required]
        public int ReservationId { get; set; }

        public string? Description { get; set; }
    }
}
