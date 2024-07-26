using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Avengers.Data;
using Avengers.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Avengers.Pages
{
    public class ManageUsersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ManageUsersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Users> Users { get; set; } = new List<Users>();
        public List<Teachers> Teachers { get; set; } = new List<Teachers>();
        public List<Students> Students { get; set; } = new List<Students>();
        public List<Subjects> Subjects { get; set; } = new List<Subjects>();
        public List<Classes> Classes { get; set; } = new List<Classes>();
        public SelectList Roles { get; set; }

        [BindProperty]
        public Users SelectedUser { get; set; } = new Users();

        public Role? UserRole { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            string? userEmail = HttpContext.Session.GetString("UserEmail");

            if (userEmail == null)
            {
                // Redirect to login if not authenticated
                return RedirectToPage("/Login");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null || user.UserRole != Role.Administrator)
            {
                // Return the page with access denied message
                UserRole = null; // or set an appropriate default value
                return Page();
            }

            UserRole = user?.UserRole;

            Users = await _context.Users.ToListAsync();
            Teachers = await _context.Teachers.ToListAsync();
            Students = await _context.Students.ToListAsync();
            Subjects = await _context.Subjects.ToListAsync();
            Classes = await _context.Classes.ToListAsync();
            Roles = new SelectList(Enum.GetValues(typeof(Role)).Cast<Role>());

            if (id.HasValue)
            {
                SelectedUser = await _context.Users.FindAsync(id.Value);
                if (SelectedUser == null)
                {
                    return NotFound();
                }
            }

            return Page();
        }


        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAssignSubjectAsync(int teacherId, int? selectedSubjectId)
        {
            var teacher = await _context.Teachers.FindAsync(teacherId);
            if (teacher != null && selectedSubjectId.HasValue)
            {
                teacher.SubjectId = selectedSubjectId.Value;
                _context.Teachers.Update(teacher);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAssignClassAsync(int studentId, int? selectedClassId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student != null && selectedClassId.HasValue)
            {
                student.ClassID = selectedClassId.Value;
                _context.Students.Update(student);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _context.Users.FindAsync(SelectedUser.Id);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = SelectedUser.Name;
            user.Email = SelectedUser.Email;
            user.UserRole = SelectedUser.UserRole;
            user.Password = SelectedUser.Password;
            user.MobilePhone = SelectedUser.MobilePhone;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
