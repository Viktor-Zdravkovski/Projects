namespace FlashGameCofe.Domain.Models
{
    public class Note : BaseEntity
    {
        public string Description { get; set; }

        public Reservation Reservation { get; set; }

        public int ReservationId { get; set; }
    }
}
