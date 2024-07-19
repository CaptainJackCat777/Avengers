namespace Avengers.Models
{
    public class Teachers
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? SunjectId { get; set; }
        public Subjects? Subject { get; set; }
    }
}
