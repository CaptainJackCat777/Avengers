using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Avengers.Data;
using Avengers.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Avengers.Pages
{
    public class HomeworkGradingModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeworkGradingModel> _logger;

        public HomeworkGradingModel(ApplicationDbContext context, ILogger<HomeworkGradingModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Homework_creation Homework_creation { get; set; } = default!;
        public Role? UserRole { get; set; }
        public string? UserEmail { get; set; }
        public int? UserId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            UserEmail = HttpContext.Session.GetString("UserEmail");

            if (UserEmail == null)
            {
                // Redirect to login if not authenticated
                return RedirectToPage("/Login");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == UserEmail);
            if (user == null || !IsUserAuthorized(user))
            {
                // Redirect to login if user is not authorized
                return RedirectToPage("/Login");
            }

            UserRole = user?.UserRole;
            UserId = user?.Id;

            if (id == null || _context.HomeworkCreations == null)
            {
                return NotFound();
            }

            var homework_creation = await _context.HomeworkCreations
                .FirstOrDefaultAsync(m => m.Id == id);

            if (homework_creation == null)
            {
                return NotFound();
            }

            Homework_creation = homework_creation;
            ViewData["HomeworkAssignmentId"] = new SelectList(_context.HomeworkAssignments, "Id", "Title");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingHomework = await _context.HomeworkCreations
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == Homework_creation.Id);

            if (existingHomework == null)
            {
                return NotFound();
            }

            // Only update the fields that need to be changed
            _context.Attach(Homework_creation).State = EntityState.Modified;

            // Ensure only specific fields are updated
            _context.Entry(Homework_creation).Property(x => x.Text).IsModified = true;
            _context.Entry(Homework_creation).Property(x => x.Grade).IsModified = true;
            _context.Entry(Homework_creation).Property(x => x.Graded).IsModified = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Homework_creationExists(Homework_creation.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }


        private bool Homework_creationExists(int id)
        {
            return (_context.HomeworkCreations?.Any(e => e.Id == id)).GetValueOrDefault();
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
