namespace Avengers.Models
{
    public enum Role
    {
        Students,
        Teachers,
        Administrator
    }

    public class Users
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public Role? UserRole { get; set; }
        public string? Password { get; set; }
        public string? MobilePhone { get; set; }
        public string? SClass { get; set; }
        public string? created { get; set; }
        public string? last_modified { get; set; }
    }
}