using FlashGameCofe.DataBase.Extensions;
using FlashGameCofe.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashGameCofe.DataBase.Context
{
    public class FlashGameCofeDbContext : DbContext
    {
        public FlashGameCofeDbContext(DbContextOptions<FlashGameCofeDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Note> Notes { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureUserEntity();

            modelBuilder.ConfigureReservationEntity();

            modelBuilder.ConfigureNoteEntity();

            base.OnModelCreating(modelBuilder);
        }
    }
}
