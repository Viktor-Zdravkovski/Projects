using Microsoft.EntityFrameworkCore;
using TodoApplication.Domain;

namespace TodoApplication.DataAccess
{
    public class TodoAppDbContext : DbContext
    {
        public DbSet<Todo> Todo { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Category> Category { get; set; }

        public TodoAppDbContext() { }

        public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>()
                .HasOne(t => t.Status)
                .WithMany()
                .HasForeignKey(t => t.StatusId);

            modelBuilder.Entity<Todo>()
                .HasOne(t => t.Category)
                .WithMany()
                .HasForeignKey(t => t.CategoryId);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Work" },
                new Category { Id = 2, Name = "Home" },
                new Category { Id = 3, Name = "Exercise" },
                new Category { Id = 4, Name = "Hobby" },
                new Category { Id = 5, Name = "Shopping" },
                new Category { Id = 6, Name = "Freetime" },
                new Category { Id = 7, Name = "Homework" }
            );

            modelBuilder.Entity<Status>().HasData(
                new Status() { Id = 1, Name = "In Progress" },
                new Status() { Id = 2, Name = "Completed" }
            );

            modelBuilder.Entity<Todo>().HasData(
                new Todo() { Id = 1, Description = "Read EF Documentation", DueDate = new DateTime(year: 2024, month: 7, day: 30), CategoryId = 1, StatusId = 1 },
                new Todo() { Id = 2, Description = "Basketball", DueDate = new DateTime(2024, 6, 25), CategoryId = 4, StatusId = 2 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
