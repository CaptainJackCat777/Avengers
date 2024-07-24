using System.ComponentModel.DataAnnotations;

namespace Avengers.Models
{
    public class HomeworkCreation
    {
        public int Id { get; set; }
        public string? Created { get; set; }
        public string? LastModified { get; set; }
        public string? Text { get; set; }
        public string? FilePath { get; set; }
        public int? StudentId { get; set; }
        public Students? Student { get; set; }
        [Required]
       
        public HomeworkAssignments HomeworkAssignment { get; set; }
    }
}
