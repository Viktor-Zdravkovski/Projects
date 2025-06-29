using Microsoft.EntityFrameworkCore;
using NotesApp.Domain.Enums;
using NotesApp.Domain.Models;

namespace NotesApp.DataAccess.Extensions
{
    public static class EFExtensions
    {
        public static void ConfigureNoteEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("Note");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Text)
                      .IsRequired()
                      .HasMaxLength(250);

                entity.Property(e => e.Priority)
                      .HasConversion<int>()
                      .HasDefaultValue(Priority.Low);

                entity.Property(e => e.Tag)
                     .HasConversion<int>();

                entity.HasOne(e => e.User)
                      .WithMany(u => u.Notes)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Note_User_UserId");
            });
        }

        public static void ConfigureUserEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.LastName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Username)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.Password)
                      .IsRequired();

                entity.Ignore(e => e.Age);

                entity.HasIndex(e => e.Username)
                      .IsUnique()
                      .HasDatabaseName("IX_User_Username");
            });
        }
    }
}
