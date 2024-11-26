using MovieRental.Domain.Enums;

namespace MovieRental.Domain
{
    public class Cast : BaseEntity
    {
        public string MovieId { get; set; }

        public string Name { get; set; }

        public Part Part { get; set; }
    }
}
