namespace Avengers.Models
{
    public class Teachers
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? SubjectId { get; set; }
        public Subjects? Subject { get; set; }
        public string? created { get; set; }
        public string? last_modified { get; set; }
    }
}
