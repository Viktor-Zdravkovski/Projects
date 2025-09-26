using HotelManagement.DataBase.Extensions;
using HotelManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.DataBase.Context
{
    public class HotelManagementDbContext : DbContext
    {
        public HotelManagementDbContext(DbContextOptions<HotelManagementDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Payment> Payment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureUserEntity();

            modelBuilder.ConfigureReservationEntity();

            modelBuilder.ConfigureRoomEntity();

            modelBuilder.ConfigurePaymentEntity();

            base.OnModelCreating(modelBuilder);
        }
    }
}
