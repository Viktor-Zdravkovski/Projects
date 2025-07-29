namespace FlashGameCofe.Dto.ReservationsDto
{
    public class UpdateReservationDto
    {
        public int Id { get; set; }

        public DateTime? StartingTime { get; set; }

        public string? NoteDescription { get; set; }
    }
}
