namespace Avengers.Models
{
    public class Assignment_x_students
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public Students? Student { get; set; }
        public int? AssignmentId { get; set; }
        public Homework_assignments? Asignemnt { get; set; }
    }
}
