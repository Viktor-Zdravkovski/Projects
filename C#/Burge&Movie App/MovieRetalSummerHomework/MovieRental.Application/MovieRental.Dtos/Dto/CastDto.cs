using MovieRental.Domain.Enums;

namespace MovieRental.Dtos.Dto
{
    public class CastDto
    {
        public int Id { get; set; }

        public string MovieId { get; set; }

        public string Name { get; set; }

        public Part Part { get; set; } 
    }
}
