namespace FlashGameCofe.Domain.Models
{
    public class Reservation : BaseEntity
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public Note? Note { get; set; }

        public DateTime StartingHour { get; set; }

        public DateTime EndingHour => StartingHour.AddHours(2);
    }
}
