using Microsoft.EntityFrameworkCore;
using NotesApp.DataAccess.Extensions;
using NotesApp.Domain.Enums;
using NotesApp.Domain.Models;

namespace NotesApp.DataAccess.Context
{
    public class NotesAppDbContext : DbContext
    {
        public NotesAppDbContext(DbContextOptions<NotesAppDbContext> options) : base(options) { }
        public DbSet<Note> Note { get; set; }
        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureNoteEntity();

            modelBuilder.ConfigureUserEntity();

            base.OnModelCreating(modelBuilder);
        }
    }
}
