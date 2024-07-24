using Avengers.Data;
using Avengers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avengers.Pages
{
    [Authorize(Roles = "Administrator")]
    public class ManageUsersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ManageUsersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Teachers> Teachers { get; set; } = new List<Teachers>();
        public List<Students> Students { get; set; } = new List<Students>();
        public List<Subjects> Subjects { get; set; } = new List<Subjects>();
        public List<Classes> Classes { get; set; } = new List<Classes>();

        public IActionResult OnGet()
        {
            if (!User.IsInRole("Administrator"))
            {
                // Redirect to index page if the user is not authorized
                return RedirectToPage("/Index");
            }

            // Fetch data if authorized
            Teachers = _context.Teachers.ToList();
            Students = _context.Students.ToList();
            Subjects = _context.Subjects.ToList();
            Classes = _context.Classes.ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAssignSubjectAsync(int teacherId, int? selectedSubjectId)
        {
            var teacher = _context.Teachers.FirstOrDefault(t => t.Id == teacherId);
            if (teacher != null)
            {
                teacher.SubjectId = selectedSubjectId;
                _context.Teachers.Update(teacher);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAssignClassAsync(int studentId, int? selectedClassId)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                student.ClassID = selectedClassId;
                _context.Students.Update(student);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
