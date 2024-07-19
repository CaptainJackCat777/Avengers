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
        public int? SunjectId { get; set; }
        public Subjects? Subject { get; set; }
        public int? TeacherId { get; set; }
        public Subjects? Teacher { get; set; }
        public string? DueDate { get; set; }
        public string? Title { get; set; }
        public Grade? Grade { get; set; }
    }
}
