using Avengers.Data;
using Avengers.Models;
using Microsoft.AspNetCore.Mvc;
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
        public int? UserId { get; set; }
        public List<Homework_creation> HomeworkCreations { get; set; } = new List<Homework_creation>();
        public List<Homework_assignments> HomeworkAssignments { get; set; } = new List<Homework_assignments>();

        public IActionResult OnGet()
        {
            UserEmail = HttpContext.Session.GetString("UserEmail");

            if (UserEmail == null)
            {
                // Redirect to login if not authenticated
                return RedirectToPage("/Login");
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == UserEmail);
            if (user == null || !IsUserAuthorized(user))
            {
                // Redirect to login if user is not authorized
                return RedirectToPage("/Login");
            }

            UserRole = user?.UserRole;
            UserId = user?.Id;

            // Get all homework creations without any restrictions
            HomeworkCreations = _context.HomeworkCreations.ToList();
            HomeworkAssignments = _context.HomeworkAssignments.ToList();

            return Page();
        }

        private bool IsUserAuthorized(Users user)
        {
            // Define your authorization logic here
            return user.UserRole == Role.Students ||
                   user.UserRole == Role.Teachers ||
                   user.UserRole == Role.Administrator;
        }
    }
}