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
    public class HomeworkGradingModel : PageModel
    {
        private readonly Avengers.Data.ApplicationDbContext _context;

        public HomeworkGradingModel(Avengers.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["HomeworkAssignmentId"] = new SelectList(_context.HomeworkAssignments, "Id", "Id");
        ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Homework_creation Homework_creation { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.HomeworkCreations == null || Homework_creation == null)
            {
                return Page();
            }

            _context.HomeworkCreations.Add(Homework_creation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
