using Microsoft.AspNetCore.Mvc;
using Avengers.Models;
using Avengers.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Avengers.Controllers
{
    public class HomeworkAssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeworkAssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HomeworkAssignments
        public async Task<IActionResult> Index()
        {
            var assignments = await _context.HomeworkAssignments
                .Include(h => h.Subject)
                .Include(h => h.Teacher)
                .ToListAsync();

            if (assignments == null || !assignments.Any())
            {
                ViewBag.Message = "No homework assignments available.";
            }

            return View(assignments);
        }

        // GET: HomeworkAssignments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HomeworkAssignments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,DueDate,SubjectId,TeacherId")] HomeworkAssignments homeworkAssignments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homeworkAssignments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homeworkAssignments);
        }

        // GET: HomeworkAssignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeworkAssignment = await _context.HomeworkAssignments
                .Include(h => h.Subject)
                .Include(h => h.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (homeworkAssignment == null)
            {
                return NotFound();
            }

            return View(homeworkAssignment);
        }

        // POST: HomeworkAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homeworkAssignment = await _context.HomeworkAssignments.FindAsync(id);
            _context.HomeworkAssignments.Remove(homeworkAssignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
