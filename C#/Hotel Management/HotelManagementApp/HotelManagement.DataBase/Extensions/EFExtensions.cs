using HotelManagement.Domain.Enums;
using HotelManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.DataBase.Extensions
{
    public static class EFExtensions
    {
        public static void ConfigureUserEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.FirstName)
                      .IsRequired();

                entity.Property(x => x.LastName)
                      .IsRequired();

                entity.Property(x => x.Email)
                      .IsRequired();

                entity.Property(x => x.PhoneNumber)
                      .IsRequired();

                entity.Property(x => x.Password)
                      .IsRequired();

                entity.Property(x => x.Role)
                      .HasDefaultValue(Roles.Customer)
                      .IsRequired();

                entity.HasMany(x => x.Reservations)
                      .WithOne(r => r.User)
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
        public static void ConfigureReservationEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("Reservations");

                entity.HasKey(x => x.Id);

                //entity.Property(x => x.Id).ValueGeneratedOnAdd();

                entity.Property(x => x.CheckedIn)
                      .IsRequired();

                entity.Property(x => x.CheckedOut)
                      .IsRequired();

                entity.Property(x => x.Status)
                      .IsRequired();

                entity.HasOne(x => x.User)
                      .WithMany(r => r.Reservations)
                      .HasForeignKey(i => i.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
        public static void ConfigureRoomEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Rooms");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id).ValueGeneratedOnAdd();

                entity.Property(x => x.RoomNumber)
                      .IsRequired();

                entity.Property(x => x.Type)
                      .IsRequired();

                entity.Property(x => x.PricePerNight)
                      .HasPrecision(6, 2)
                      .IsRequired();

                entity.Property(x => x.Status)
                      .IsRequired();

                entity.HasMany(x => x.Reservations)
                      .WithOne(r => r.Room)
                      .HasForeignKey(i => i.RoomId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public static void ConfigurePaymentEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payments");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Amount)
                      .HasPrecision(6, 2)
                      .IsRequired();

                entity.Property(x => x.Method)
                      .IsRequired();

                entity.Property(x => x.PaidAt)
                      .IsRequired();

                entity.HasOne(x => x.Reservation)
                      .WithOne(r => r.Payment)
                      .HasForeignKey<Payment>(i => i.ReservationId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
