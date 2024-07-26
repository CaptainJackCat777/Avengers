using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Avengers.Data;
using Avengers.Models;

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

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["HomeworkAssignmentId"] = new SelectList(_context.HomeworkAssignments, "Id", "Title");

            // Initialize a new Homework_creation object for new entries
            Homework_creation = new Homework_creation();

            // For existing entries, you would need to handle the case where you pass an ID or some other means of identifying the record
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
