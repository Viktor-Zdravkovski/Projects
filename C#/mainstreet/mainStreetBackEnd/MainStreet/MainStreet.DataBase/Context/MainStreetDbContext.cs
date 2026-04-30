using MainStreet.DataBase.Extensions;
using MainStreet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MainStreet.DataBase.Context
{
    public class MainStreetDbContext : DbContext
    {
        public MainStreetDbContext(DbContextOptions<MainStreetDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureUserEntity();

            modelBuilder.ConfigureOrderEntity();

            modelBuilder.ConfigurePaymentEntity();

            base.OnModelCreating(modelBuilder);
        }
    }
}
