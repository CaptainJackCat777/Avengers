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

    public class HomeworkAssignments
    {
        public int Id { get; set; }
        public int? SubjectId { get; set; }
        public Subjects? Subject { get; set; }
        public int? TeacherId { get; set; }
        public Subjects? Teacher { get; set; }
        public string? DueDate { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; } // Make sure this is here
        public Grade? Grade { get; set; }
        public ICollection<Students>? Students { get; set; }
    }
}
