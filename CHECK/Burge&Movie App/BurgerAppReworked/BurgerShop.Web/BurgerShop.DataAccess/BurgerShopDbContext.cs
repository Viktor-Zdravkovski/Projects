using BurgerShop.Domain;
using Microsoft.EntityFrameworkCore;

namespace BurgerShop.DataBase
{
    public class BurgerShopDbContext : DbContext
    {
        public DbSet<Burger> Burgers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Location> Locations { get; set; }

        public BurgerShopDbContext()
        {

        }

        public BurgerShopDbContext(DbContextOptions<BurgerShopDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Burger>().HasData(
               new Burger()
               {
                   Id = 1,
                   OrderId = 1,
                   Name = "Classic Beef Burger",
                   Price = 10,
                   IsVegan = false,
                   IsVegetarian = false,
                   HasFries = true
               },

                new Burger()
                {
                    Id = 2,
                    OrderId = 2,
                    Name = "Vegan Delight",
                    Price = 12,
                    IsVegan = true,
                    IsVegetarian = true,
                    HasFries = false
                },

                new Burger()
                {
                    Id = 3,
                    OrderId = 3,
                    Name = "Chicken Burger",
                    Price = 11,
                    IsVegan = false,
                    IsVegetarian = false,
                    HasFries = true
                },

                new Burger()
                {
                    Id = 4,
                    OrderId = 4,
                    Name = "Cheese Veggie Burger",
                    Price = 9,
                    IsVegan = false,
                    IsVegetarian = true,
                    HasFries = false
                },

                new Burger()
                {
                    Id = 5,
                    OrderId = 5,
                    Name = "Spicy Black Bean Burger",
                    Price = 8,
                    IsVegan = true,
                    IsVegetarian = true,
                    HasFries = true
                }
                );

            modelBuilder.Entity<Order>().HasData(

                new Order()
                {
                    Id = 1,
                    FullName = "John Doe",
                    Address = "123 Main St, Springfield",
                    IsDelivered = true,
                    LocationId = 1,
                    
                },
                new Order()
                {
                    Id = 2,
                    FullName = "Jane Smith",
                    Address = "456 Oak Ave, Metropolis",
                    IsDelivered = false,
                    LocationId = 2,
                    
                },
                new Order()
                {
                    Id = 3,
                    FullName = "Alice Johnson",
                    Address = "789 Pine Rd, Gotham",
                    IsDelivered = true,
                    LocationId = 3,
                    
                },
                new Order()
                {
                    Id = 4,
                    FullName = "Bob Brown",
                    Address = "101 Maple Dr, Star City",
                    IsDelivered = false,
                    LocationId = 4,
                    
                },
                new Order()
                {
                    Id = 5,
                    FullName = "Charlie Green",
                    Address = "202 Elm St, Smallville",
                    IsDelivered = true,
                    LocationId = 5,
                    
                }
                );

            modelBuilder.Entity<Location>().HasData(

                new Location()
                {
                    Id = 1,
                    Name = "Downtown Diner",
                    Address = "123 Main St, Springfield",
                    OpensAt = DateTime.Today.Add(new TimeSpan(9, 0, 0)),
                    ClosesAt = DateTime.Today.Add(new TimeSpan(21, 0, 0))
                },

                new Location()
                {
                    Id = 2,
                    Name = "Seaside Cafe",
                    Address = "456 Ocean Blvd, Beach City",
                    OpensAt = DateTime.Today.Add(new TimeSpan(9, 0, 0)),
                    ClosesAt = DateTime.Today.Add(new TimeSpan(23, 0, 0))
                },

                new Location()
                {
                    Id = 3,
                    Name = "Mountain View Restaurant",
                    Address = "789 Peak Rd, Alpine Town",
                    OpensAt = DateTime.Today.Add(new TimeSpan(8, 0, 0)),
                    ClosesAt = DateTime.Today.Add(new TimeSpan(23, 0, 0))
                },

                new Location()
                {
                    Id = 4,
                    Name = "City Lights Bistro",
                    Address = "101 Skyline Dr, Metropolis",
                    OpensAt = DateTime.Today.Add(new TimeSpan(9, 0, 0)),
                    ClosesAt = DateTime.Today.Add(new TimeSpan(00, 0, 0))
                },

                new Location()
                {
                    Id = 5,
                    Name = "Riverside Grill",
                    Address = "202 Riverbank Ave, Water Town",
                    OpensAt = DateTime.Today.Add(new TimeSpan(7, 0, 0)),
                    ClosesAt = DateTime.Today.Add(new TimeSpan(23, 0, 0))
                }
                );



            base.OnModelCreating(modelBuilder);
        }
    }
}
