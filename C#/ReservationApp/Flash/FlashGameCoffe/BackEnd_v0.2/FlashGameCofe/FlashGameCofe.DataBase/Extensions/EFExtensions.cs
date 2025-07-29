using FlashGameCofe.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashGameCofe.DataBase.Extensions
{
    public static class EFExtensions
    {
        public static void ConfigureUserEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(t => t.Id);

                entity.Property(x => x.FirstName)
                      .IsRequired();

                entity.Property(x => x.LastName)
                      .IsRequired();

                entity.Property(x => x.Email)
                      .IsRequired();

                entity.Property(x => x.Password)
                      .IsRequired();

                entity.Property(x => x.PhoneNumber)
                      .IsRequired();

                entity.Property(x => x.Role)
                      .IsRequired();

                entity.HasMany(u => u.Reservations)
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

                entity.HasKey(t => t.Id);

                entity.Property(x => x.Id).ValueGeneratedOnAdd();

                entity.Property(x => x.StartingTime)
                      .IsRequired();

                entity.Property(x => x.NoteDescription)
                      .IsRequired(false);

                entity.HasOne(r => r.User)
                      .WithMany(u => u.Reservations)
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
