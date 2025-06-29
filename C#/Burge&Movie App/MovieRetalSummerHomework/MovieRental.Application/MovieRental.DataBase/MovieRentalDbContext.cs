using Microsoft.EntityFrameworkCore;
using MovieRental.Domain;
using MovieRental.Domain.Enums;
using System.Runtime.Intrinsics.X86;

namespace MovieRental.DataBase
{
    public class MovieRentalDbContext : DbContext
    {
        public DbSet<Movie> Movie { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Rental> Rental { get; set; }

        public MovieRentalDbContext()
        {

        }

        public MovieRentalDbContext(DbContextOptions<MovieRentalDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(
                new Movie()
                {
                    Id = 1,
                    Title = "DeadPool & Wolverine",
                    Genre = Genre.Action,
                    Language = Language.English,
                    IsAvailable = true,
                    ReleaseDate = new DateTime(2020, 1, 1),
                    Length = new TimeSpan(2, 0, 0),
                    AgeRestriction = 15,
                    Quantity = 10
                },

                new Movie()
                {
                    Id = 2,
                    Title = "Mother of the bride",
                    Genre = Genre.Comedy,
                    Language = Language.Spanish,
                    IsAvailable = true,
                    ReleaseDate = new DateTime(2021, 2, 15),
                    Length = new TimeSpan(1, 45, 0),
                    AgeRestriction = 10,
                    Quantity = 5
                },

                new Movie()
                {
                    Id = 3,
                    Title = "Planet of the Apes",
                    Genre = Genre.SciFi,
                    Language = Language.French,
                    IsAvailable = false,
                    ReleaseDate = new DateTime(2022, 5, 20),
                    Length = new TimeSpan(2, 30, 0),
                    AgeRestriction = 12,
                    Quantity = 8
                },

                new Movie()
                {
                    Id = 4,
                    Title = "Parasite",
                    Genre = Genre.Drama,
                    Language = Language.German,
                    IsAvailable = true,
                    ReleaseDate = new DateTime(2019, 5, 30),
                    Length = new TimeSpan(2, 12, 0), // 2 hours 12 minutes
                    AgeRestriction = 15,
                    Quantity = 3
                },

                new Movie()
                {
                    Id = 5,
                    Title = "The Matrix",
                    Genre = Genre.Action,
                    Language = Language.English,
                    IsAvailable = true,
                    ReleaseDate = new DateTime(1999, 3, 31),
                    Length = new TimeSpan(2, 16, 0), // 2 hours 16 minutes
                    AgeRestriction = 15,
                    Quantity = 10
                }
                );


            modelBuilder.Entity<User>().HasData(

                new User()
                {
                    Id = 1,
                    FullName = "Alice Johnson",
                    Email = "AliceJohnson@gmail.com",
                    Age = 28,
                    CardNumber = "1234-5678-9012-3456",
                    CreatedOn = new DateTime(2023, 1, 15),
                    IsSubscriptionExpired = false,
                    SubscriptionType = SubscriptionType.Yearly
                },

                new User()
                {
                    Id = 2,
                    FullName = "Bob Smith",
                    Email = "BobSmith@gmail.com",
                    Age = 34,
                    CardNumber = "2345-6789-0123-4567",
                    CreatedOn = new DateTime(2022, 7, 22),
                    IsSubscriptionExpired = true,
                    SubscriptionType = SubscriptionType.Monthly
                },

                new User()
                {
                    Id = 3,
                    FullName = "Charlie Brown",
                    Email = "CharlieBrown@gmail.com",
                    Age = 45,
                    CardNumber = "3456-7890-1234-5678",
                    CreatedOn = new DateTime(2024, 3, 10),
                    IsSubscriptionExpired = false,
                    SubscriptionType = SubscriptionType.Monthly
                },

                new User()
                {
                    Id = 4,
                    FullName = "Diana Prince",
                    Email = "DianaPrince@gmail.com",
                    Age = 29,
                    CardNumber = "4567-8901-2345-6789",
                    CreatedOn = new DateTime(2023, 11, 5),
                    IsSubscriptionExpired = false,
                    SubscriptionType = SubscriptionType.Monthly
                },

                new User()
                {
                    Id = 5,
                    FullName = "Edward Nygma",
                    Email = "EdwardNygma@gmail.com",
                    Age = 38,
                    CardNumber = "5678-9012-3456-7890",
                    CreatedOn = new DateTime(2021, 9, 25),
                    IsSubscriptionExpired = true,
                    SubscriptionType = SubscriptionType.Yearly
                }
                );

            modelBuilder.Entity<Rental>().HasData(
                new Rental()
                {
                    Id = 1,
                    MovieId = 1,
                    UserId = 1,
                    RentedOn = new DateTime(2024, 7, 1),
                    ReturnedOn = new DateTime(2024, 7, 5)
                },

                new Rental()
                {
                    Id = 2,
                    MovieId = 2,
                    UserId = 2,
                    RentedOn = new DateTime(2024, 7, 2),
                    ReturnedOn = new DateTime(2024, 7, 6)
                },

                new Rental()
                {
                    Id = 3,
                    MovieId = 3,
                    UserId = 3,
                    RentedOn = new DateTime(2024, 7, 3),
                    ReturnedOn = new DateTime(2024, 7, 7)
                },

                new Rental()
                {
                    Id = 4,
                    MovieId = 4,
                    UserId = 4,
                    RentedOn = new DateTime(2024, 7, 4),
                    ReturnedOn = new DateTime(2024, 7, 8)
                },

                new Rental()
                {
                    Id = 5,
                    MovieId = 5,
                    UserId = 5,
                    RentedOn = new DateTime(2024, 7, 5),
                    ReturnedOn = new DateTime(2024, 7, 9)
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
