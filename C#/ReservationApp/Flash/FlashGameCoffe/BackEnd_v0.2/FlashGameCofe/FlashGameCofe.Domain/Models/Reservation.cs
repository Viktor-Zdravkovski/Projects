namespace FlashGameCofe.Domain.Models
{
    public class Reservation : BaseEntity
    {
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;

        public string? NoteDescription { get; set; }

        public DateTime StartingTime { get; set; }
    }
}
