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
    public class HomeworkAssignmentModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeworkAssignmentModel(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnGet()
        {
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Homework_assignments Homework_assignments { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.HomeworkAssignments == null || Homework_assignments == null)
            {
                return Page();
            }

            _context.HomeworkAssignments.Add(Homework_assignments);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
