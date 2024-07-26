using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Avengers.Data;
using Avengers.Models;

namespace Avengers.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Users Users { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Users == null || Users == null)
            {
                return Page();
            }

            // Add user to Users table
            _context.Users.Add(Users);
            await _context.SaveChangesAsync();

            // Add user to the corresponding role table
            if (Users.UserRole == Role.Students)
            {
                var student = new Students
                {
                    Name = Users.Name,
                    created = DateTime.Now.ToString(),
                    last_modified = DateTime.Now.ToString()
                    // Assign other properties as needed
                };
                _context.Students.Add(student);
            }
            else if (Users.UserRole == Role.Teachers)
            {
                var teacher = new Teachers
                {
                    Name = Users.Name,
                    created = DateTime.Now.ToString(),
                    last_modified = DateTime.Now.ToString()
                    // Assign other properties as needed
                };
                _context.Teachers.Add(teacher);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
