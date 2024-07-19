namespace Avengers.Models
{
    public class Students
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ClassID { get; set; }
        public Classes? Class { get; set; }
        public string? created { get; set; }
        public string? last_modified { get; set; }
        public ICollection<Homework_assignments>? Assignments { get; set; }
    }
}
