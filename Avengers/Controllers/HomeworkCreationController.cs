using Microsoft.AspNetCore.Mvc;
using Avengers.Models;
using Avengers.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Avengers.Controllers
{
    public class HomeworkCreationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeworkCreationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HomeworkCreation/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var homework = await _context.HomeworkCreations
                .Include(h => h.HomeworkAssignment)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (homework == null)
            {
                return NotFound();
            }

            return View(homework);
        }

        // GET: HomeworkCreation/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var homework = await _context.HomeworkCreations.FindAsync(id);

            if (homework == null)
            {
                return NotFound();
            }

            return View(homework);
        }

        // POST: HomeworkCreation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text,FilePath,StudentId,HomeworkAssignmentId")] HomeworkCreation homeworkCreation)
        {
            if (id != homeworkCreation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homeworkCreation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeworkCreationExists(homeworkCreation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Details), new { id = homeworkCreation.Id });
            }

            return View(homeworkCreation);
        }

        private bool HomeworkCreationExists(int id)
        {
            return _context.HomeworkCreations.Any(e => e.Id == id);
        }
    }
}
