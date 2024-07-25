namespace Avengers.Models
{
    public class Homework_creation
    {
        public int Id { get; set; }
        public string? Created { get; set; }
        public string? LastModified { get; set; }
        public string? Text { get; set; }
        public string? FilePath { get; set; }
        public int? StudentId { get; set; }
        public Grade? Grade { get; set; }
        public Students? Student { get; set; }

        public int? HomeworkAssignmentId { get; set; }  // Foreign key property
        public Homework_assignments? HomeworkAssignment { get; set; }  // Navigation property

        public bool Submitted { get; set; } // New property
        public bool Graded { get; set; }    // New property
    }
}
