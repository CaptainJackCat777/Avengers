namespace Avengers.Models
{
    public enum Grade
    {
        PoorMark,
        NeedImprovementMark,
        OKMark,
        VeryGoodMark,
        ExcellentMark
    }

    public class Homework_assignments
    {
        public int Id { get; set; }
        public int? SubjectId { get; set; }
        public Subjects? Subject { get; set; }
        public int? TeacherId { get; set; }
        public Subjects? Teacher { get; set; }
        public string? DueDate { get; set; }
        public string? Title { get; set; }

        public ICollection<Students>? Students { get; set; }
        public ICollection<Homework_creation>? HomeworkCreations { get; set; }  // Add this collection
    }
}
