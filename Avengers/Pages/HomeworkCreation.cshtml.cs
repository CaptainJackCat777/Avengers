using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Avengers.Data;
using Avengers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Avengers.Pages
{
    public class HomeworkCreationModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public HomeworkCreationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Homework_creation Homework_creation { get; set; } = default!;

        public bool IsEditable { get; set; } = true;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["HomeworkAssignmentId"] = new SelectList(_context.HomeworkAssignments, "Id", "Title");

            if (id == null)
            {
                // New homework creation
                return Page();
            }

            var homework_creation = await _context.HomeworkCreations.FirstOrDefaultAsync(m => m.Id == id);
            if (homework_creation == null)
            {
                return NotFound();
            }

            Homework_creation = homework_creation;

            // Check user role and submission status
            if (User.IsInRole("Student") && Homework_creation.Submitted)
            {
                IsEditable = false;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["HomeworkAssignmentId"] = new SelectList(_context.HomeworkAssignments, "Id", "Title");
                return Page();
            }

            if (Homework_creation.Id == 0)
            {
                // New homework creation
                Homework_creation.Created = DateTime.Now.ToString();
                Homework_creation.LastModified = DateTime.Now.ToString();
                Homework_creation.StudentId = 1; // Replace with actual student ID

                _context.HomeworkCreations.Add(Homework_creation);
            }
            else
            {
                // Existing homework creation update
                var homeworkToUpdate = await _context.HomeworkCreations.FindAsync(Homework_creation.Id);

                if (homeworkToUpdate == null)
                {
                    return NotFound();
                }

                // Update properties
                homeworkToUpdate.Text = Homework_creation.Text;
                homeworkToUpdate.HomeworkAssignmentId = Homework_creation.HomeworkAssignmentId;
                homeworkToUpdate.Submitted = Homework_creation.Submitted;
                homeworkToUpdate.LastModified = DateTime.Now.ToString();
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
