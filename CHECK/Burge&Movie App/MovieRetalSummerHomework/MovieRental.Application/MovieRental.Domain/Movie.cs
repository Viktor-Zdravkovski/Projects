using MovieRental.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MovieRental.Domain
{
    public class Movie : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        public Genre Genre { get; set; } 

        public Language Language { get; set; } 

        public bool IsAvailable { get; set; }

        public DateTime ReleaseDate { get; set; }

        public TimeSpan Length { get; set; }

        public int AgeRestriction { get; set; }

        public int Quantity { get; set; }
    }
}
