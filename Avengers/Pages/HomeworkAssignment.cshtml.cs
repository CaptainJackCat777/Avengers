using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Avengers.Models;
using Avengers.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Avengers.Pages
{
    public class HomeworkAssignmentModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public HomeworkAssignmentModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<HomeworkAssignments> HomeworkAssignments { get; set; }
        [BindProperty]
        public HomeworkAssignments NewHomeworkAssignment { get; set; }
        [BindProperty]
        public List<IFormFile> Files { get; set; }
        public string Message { get; set; }
        public SelectList SubjectsList { get; set; }
        public SelectList TeachersList { get; set; }

        public async Task OnGetAsync()
        {
            HomeworkAssignments = await _context.HomeworkAssignments
                .Include(h => h.Subject)
                .Include(h => h.Teacher)
                .ToListAsync();

            SubjectsList = new SelectList(await _context.Subjects.ToListAsync(), "Id", "Name");
            TeachersList = new SelectList(await _context.Subjects.ToListAsync(), "Id", "Name");

            if (HomeworkAssignments == null || !HomeworkAssignments.Any())
            {
                Message = "No homework assignments available.";
            }
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                SubjectsList = new SelectList(await _context.Subjects.ToListAsync(), "Id", "Name");
                TeachersList = new SelectList(await _context.Subjects.ToListAsync(), "Id", "Name");
                return Page();
            }

            var subject = await _context.Subjects.FindAsync(NewHomeworkAssignment.SubjectId);
            if (subject == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Subject.");
                return Page();
            }

            var teacher = await _context.Subjects.FindAsync(NewHomeworkAssignment.TeacherId);
            if (teacher == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Teacher.");
                return Page();
            }

            NewHomeworkAssignment.Subject = subject;
            NewHomeworkAssignment.Teacher = teacher;

            // Handle file uploads separately
            var filePaths = new List<string>();
            foreach (var file in Files)
            {
                if (file.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/uploads", file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    filePaths.Add("/uploads/" + file.FileName);
                }
            }

            NewHomeworkAssignment.Description = NewHomeworkAssignment.Description;  // Ensure Description is set

            _context.HomeworkAssignments.Add(NewHomeworkAssignment);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var homeworkAssignment = await _context.HomeworkAssignments.FindAsync(id);
            if (homeworkAssignment != null)
            {
                _context.HomeworkAssignments.Remove(homeworkAssignment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
