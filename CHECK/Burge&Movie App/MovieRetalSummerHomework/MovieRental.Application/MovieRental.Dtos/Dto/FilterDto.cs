namespace MovieRental.Dtos.Dto
{
    public class FilterDto
    {
        public List<MovieDto> Movie { get; set; }

        public int MovieId { get; set; }

        public int RentalId { get; set; }
    }
}
