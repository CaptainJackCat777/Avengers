namespace Avengers.Models
{
    public class Homework_creation
    {
        public int Id { get; set; }
        public string? created { get; set; }
        public string? last_modified { get; set; }
        public string? text { get; set; }
        public string? file_path { get; set; }
        public int? StudentId { get; set; }
        public Students? Student { get; set; }
    }
}
