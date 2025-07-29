using System.ComponentModel.DataAnnotations;

namespace FlashGameCofe.Dto.ReservationsDto
{
    public class AddReservationDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime StartingTime { get; set; }

        public string? NoteDescription { get; set; }

        //[Required]
        //public string PhoneNumber { get; set; }
    }
}
