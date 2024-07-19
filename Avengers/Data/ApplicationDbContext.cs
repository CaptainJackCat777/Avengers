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

        // DbSets for your models

        public DbSet<Avengers.Models.Students>? Students { get; set; }

        // DbSets for your models

        public DbSet<Avengers.Models.Subjects>? Subjects { get; set; }



        // DbSets for your models

        public DbSet<Avengers.Models.Teachers>? Teachers { get; set; }

        // DbSets for your models

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API configurations (optional)

            // Example: Configure relationships and keys
            modelBuilder.Entity<Assignment_x_students>()
                .HasOne(a => a.Student)
                .WithMany()
                .HasForeignKey(a => a.StudentId);

            modelBuilder.Entity<Assignment_x_students>()
                .HasOne(a => a.Asignemnt)
                .WithMany()
                .HasForeignKey(a => a.AssignmentId);

            modelBuilder.Entity<Homework_assignments>()
                .HasOne(h => h.Subject)
                .WithMany()
                .HasForeignKey(h => h.SunjectId);

            modelBuilder.Entity<Homework_assignments>()
                .HasOne(h => h.Teacher)
                .WithMany()
                .HasForeignKey(h => h.TeacherId);

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
                .HasForeignKey(t => t.SunjectId);
        }

        // DbSets for your models

        public DbSet<Avengers.Models.Homework_assignments>? Homework_assignments { get; set; }
    }
}