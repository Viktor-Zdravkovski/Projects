using MainStreet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MainStreet.DataBase.Extensions
{
    public static class EFExtensions
    {
        public static void ConfigureUserEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(x => x.Id);

                entity.HasIndex(x => x.Email).IsUnique();

                entity.Property(x => x.FirstName)
                      .IsRequired()
                      .HasMaxLength(30);

                entity.Property(x => x.LastName)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(x => x.Email)
                      .IsRequired()
                      .HasMaxLength(256);

                entity.Property(x => x.Password)
                      .IsRequired();

                entity.Property(x => x.PhoneNumber)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(x => x.Role)
                      .IsRequired()
                      .HasConversion<int>();
            });
        }

        public static void ConfigureOrderEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.CustomerName)
                      .HasMaxLength(100);

                entity.Property(x => x.Status)
                              .IsRequired()
                              .HasConversion<int>();

                entity.HasOne(o => o.User)
                  .WithMany(u => u.Orders)
                  .HasForeignKey(o => o.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
            });
        }

        public static void ConfigurePaymentEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payments");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.PaidAt)
                      .IsRequired();

                entity.HasOne<Order>()
                                  .WithOne()
                                  .HasForeignKey<Payment>(p => p.OrderId)
                                  .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
