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

        public Homework_creation Homework_creation { get; set; } = new Homework_creation();
        public Homework_assignments? HomeworkAssignment { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HomeworkAssignment = _context.HomeworkAssignments.FirstOrDefault(h => h.Id == id);

            if (HomeworkAssignment == null)
            {
                return NotFound();
            }

            // Initialize other necessary data
            ViewData["HomeworkAssignmentId"] = new SelectList(_context.HomeworkAssignments, "Id", "Title", HomeworkAssignment.Id);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name");

            return Page();
        }

        [BindProperty]
        public Homework_creation HomeworkCreation { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.HomeworkCreations == null || Homework_creation == null)
            {
                return Page();
            }

            _context.HomeworkCreations.Add(HomeworkCreation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
