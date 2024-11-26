namespace MovieRental.Dtos.Dto
{
    public class RentalDto
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public int UserId { get; set; }

        public DateTime RentedOn { get; set; }

        public DateTime? ReturnedOn { get; set; }

        //public bool IsRented { get; set; }
    }
}
