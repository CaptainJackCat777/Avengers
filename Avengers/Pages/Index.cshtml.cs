using Avengers.Data;
using Avengers.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Avengers.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public string? UserEmail { get; set; }
        public Role? UserRole { get; set; }
        public int? UserId { get; set; } // Add this property
        public List<Homework_creation> HomeworkCreations { get; set; } = new List<Homework_creation>();
        public List<Homework_assignments> HomeworkAssignments { get; set; } = new List<Homework_assignments>();

        public void OnGet()
        {
            UserEmail = HttpContext.Session.GetString("UserEmail");

            // Fetch the user from the database
            var user = _context.Users.FirstOrDefault(u => u.Email == UserEmail);
            UserRole = user?.UserRole;
            UserId = user?.Id; // Set UserId property

            // Fetch data from the database
            HomeworkCreations = _context.HomeworkCreations.ToList();
            HomeworkAssignments = _context.HomeworkAssignments.ToList();
        }
    }
}
