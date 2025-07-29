using System.ComponentModel.DataAnnotations;

namespace FlashGameCofe.Dto.NoteDto
{
    public class NotesDto
    {
        public int Id { get; set; }

        [Required]
        public int ReservationId { get; set; }

        public string? Description { get; set; }
    }
}
