using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Avengers.Models;

namespace Avengers.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for your models
        public DbSet<Classes> Classes { get; set; }
        public DbSet<Homework_assignments> HomeworkAssignments { get; set; }
        public DbSet<Homework_creation> HomeworkCreations { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API configurations (optional)

            // Example: Configure relationships and keys

            modelBuilder.Entity<Homework_assignments>()
                .HasOne(h => h.Subject)
                .WithMany()
                .HasForeignKey(h => h.SubjectId);

            modelBuilder.Entity<Homework_assignments>()
                .HasOne(h => h.Teacher)
                .WithMany()
                .HasForeignKey(h => h.TeacherId);

            modelBuilder.Entity<Homework_assignments>()
                .HasMany(h => h.Students)
                .WithMany(s => s.Assignments);

            modelBuilder.Entity<Homework_creation>()
                .HasOne(h => h.Student)
                .WithMany()
                .HasForeignKey(h => h.StudentId);

            modelBuilder.Entity<Students>()
                .HasOne(s => s.Class)
                .WithMany()
                .HasForeignKey(s => s.ClassID);

            modelBuilder.Entity<Teachers>()
                .HasOne(t => t.Subject)
                .WithMany()
                .HasForeignKey(t => t.SubjectId);

            modelBuilder.Entity<Subjects>().HasData(
               new Subjects { Id = 1, Name = "Mathematics", created = DateTime.UtcNow.ToString("o"), last_modified = DateTime.UtcNow.ToString("o") },
               new Subjects { Id = 2, Name = "English", created = DateTime.UtcNow.ToString("o"), last_modified = DateTime.UtcNow.ToString("o") },
               new Subjects { Id = 3, Name = "Geography", created = DateTime.UtcNow.ToString("o"), last_modified = DateTime.UtcNow.ToString("o") },
               new Subjects { Id = 4, Name = "History", created = DateTime.UtcNow.ToString("o"), last_modified = DateTime.UtcNow.ToString("o") },
               new Subjects { Id = 5, Name = "Physics", created = DateTime.UtcNow.ToString("o"), last_modified = DateTime.UtcNow.ToString("o") },
               new Subjects { Id = 6, Name = "Information Technology", created = DateTime.UtcNow.ToString("o"), last_modified = DateTime.UtcNow.ToString("o") },
               new Subjects { Id = 7, Name = "Bulgarian", created = DateTime.UtcNow.ToString("o"), last_modified = DateTime.UtcNow.ToString("o") },
               new Subjects { Id = 8, Name = "Biology", created = DateTime.UtcNow.ToString("o"), last_modified = DateTime.UtcNow.ToString("o") },
               new Subjects { Id = 9, Name = "Physical Education", created = DateTime.UtcNow.ToString("o"), last_modified = DateTime.UtcNow.ToString("o") }
           );
        }

    }
}