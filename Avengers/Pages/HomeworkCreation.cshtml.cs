using Avengers.Data;
using Avengers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avengers.Pages
{
    public class HomeworkCreationModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public HomeworkCreationModel(ApplicationDbContext context)
        {
            _context = context;
            HomeworkCreations = new List<HomeworkCreation>(); // Initialize the list
        }

        [BindProperty]
        public HomeworkCreation HomeworkCreation { get; set; }

        public List<HomeworkCreation> HomeworkCreations { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            HomeworkCreations = await _context.HomeworkCreations
                .Include(h => h.HomeworkAssignment) // Ensure related assignment details are included
                .ToListAsync();

            if (id.HasValue)
            {
                var homeworkAssignment = await _context.HomeworkAssignments
                    .FirstOrDefaultAsync(h => h.Id == id.Value);

                if (homeworkAssignment == null)
                {
                    return NotFound();
                }

                HomeworkCreation = new HomeworkCreation
                {
                    HomeworkAssignment = homeworkAssignment,
                    
                };
            }
            else
            {
                HomeworkCreation = new HomeworkCreation
                {
                    HomeworkAssignment = new HomeworkAssignments(), // Initialize with a new assignment
                     // Default to 0 or appropriate value
                };
            }

            return Page();
        }


        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = string.Join("\n", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                var errorDetails = ModelState.Keys.Select(key => new
                {
                    Key = key,
                    Errors = ModelState[key].Errors.Select(e => e.ErrorMessage).ToArray()
                });

                return new JsonResult(new
                {
                    success = false,
                    message = errorMessage,
                    details = errorDetails
                });
            }

            try
            {
                var homeworkAssignment = await _context.HomeworkAssignments
                    .FirstOrDefaultAsync(h => h.Id == HomeworkCreation.HomeworkAssignment.Id);

                if (homeworkAssignment == null)
                {
                    ModelState.AddModelError("HomeworkCreation.HomeworkAssignmentId", "Invalid HomeworkAssignmentId");
                    return new JsonResult(new { success = false, message = "Invalid HomeworkAssignmentId" });
                }

                var existingHomeworkCreation = await _context.HomeworkCreations
                    .FirstOrDefaultAsync(h => h.Id == HomeworkCreation.Id);

                if (existingHomeworkCreation != null)
                {
                    existingHomeworkCreation.Text = HomeworkCreation.Text;
                    existingHomeworkCreation.LastModified = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    existingHomeworkCreation.FilePath = "path/to/file";
                    existingHomeworkCreation.StudentId = 1;
                    existingHomeworkCreation.HomeworkAssignment.Id = HomeworkCreation.HomeworkAssignment.Id;

                    _context.Update(existingHomeworkCreation);
                }
                else
                {
                    HomeworkCreation.Created = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    HomeworkCreation.LastModified = HomeworkCreation.Created;
                    HomeworkCreation.FilePath = "path/to/file";
                    HomeworkCreation.StudentId = 1;
                    HomeworkCreation.HomeworkAssignment.Id = HomeworkCreation.HomeworkAssignment.Id;

                    _context.HomeworkCreations.Add(HomeworkCreation);
                }

                await _context.SaveChangesAsync();

                HomeworkCreations = await _context.HomeworkCreations
                    .Include(h => h.HomeworkAssignment)
                    .ToListAsync();

                return new JsonResult(new
                {
                    success = true,
                    message = "Changes saved successfully!",
                    homeworkCreations = HomeworkCreations
                });
            }
            catch (DbUpdateException ex)
            {
                var errorMessage = "A database update error occurred: " + ex.Message;
                return new JsonResult(new { success = false, message = errorMessage });
            }
            catch (Exception ex)
            {
                var errorMessage = "An unexpected error occurred: " + ex.Message;
                return new JsonResult(new { success = false, message = errorMessage });
            }
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = string.Join("\n", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                var errorDetails = ModelState.Keys.Select(key => new
                {
                    Key = key,
                    Errors = ModelState[key].Errors.Select(e => e.ErrorMessage).ToArray()
                });

                return new JsonResult(new
                {
                    success = false,
                    message = errorMessage,
                    details = errorDetails
                });
            }

            try
            {
                var homeworkAssignment = await _context.HomeworkAssignments
                    .FirstOrDefaultAsync(h => h.Id == HomeworkCreation.HomeworkAssignment.Id);

                if (homeworkAssignment == null)
                {
                    ModelState.AddModelError("HomeworkCreation.HomeworkAssignmentId", "Invalid HomeworkAssignmentId");
                    return new JsonResult(new { success = false, message = "Invalid HomeworkAssignmentId" });
                }

                HomeworkCreation.Created = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                HomeworkCreation.LastModified = HomeworkCreation.Created;
                HomeworkCreation.FilePath = "path/to/file";
                HomeworkCreation.StudentId = 1;
                HomeworkCreation.HomeworkAssignment.Id = HomeworkCreation.HomeworkAssignment.Id;

                _context.HomeworkCreations.Add(HomeworkCreation);
                await _context.SaveChangesAsync();

                HomeworkCreations = await _context.HomeworkCreations
                    .Include(h => h.HomeworkAssignment)
                    .ToListAsync();

                return new JsonResult(new
                {
                    success = true,
                    message = "Homework created successfully!",
                    homeworkCreations = HomeworkCreations
                });
            }
            catch (DbUpdateException ex)
            {
                var errorMessage = "A database update error occurred: " + ex.Message;
                return new JsonResult(new { success = false, message = errorMessage });
            }
            catch (Exception ex)
            {
                var errorMessage = "An unexpected error occurred: " + ex.Message;
                return new JsonResult(new { success = false, message = errorMessage });
            }
        }


    }
}
