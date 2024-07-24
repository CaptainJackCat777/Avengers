namespace Avengers.Models
{
    public class Homework_creation
    {
        public int Id { get; set; }
        public string? Created { get; set; } // Updated property name
        public string? LastModified { get; set; } // Updated property name
        public string? Text { get; set; }
        public string? FilePath { get; set; } // Updated property name
        public int? StudentId { get; set; }
        public Grade? Grade { get; set; }
        public Students? Student { get; set; }

        public int? HomeworkAssignmentId { get; set; }  // Foreign key property
        public Homework_assignments? HomeworkAssignment { get; set; }  // Navigation property
    }
}
