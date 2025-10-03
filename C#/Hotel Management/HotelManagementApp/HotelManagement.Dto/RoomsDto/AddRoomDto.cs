using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Dto.RoomsDto
{
    public class AddRoomDto
    {
        [Required]
        public string RoomNumber { get; set; }

        [Required]
        public string Type { get; set; } // dbl bed, single bed

        [Required]
        public decimal PricePerNight { get; set; }
    }
}
