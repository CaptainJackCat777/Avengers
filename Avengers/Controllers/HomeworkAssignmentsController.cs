using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Avengers.Data;
using Avengers.Models;

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
            var applicationDbContext = _context.HomeworkAssignments.Include(h => h.Subject).Include(h => h.Teacher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HomeworkAssignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HomeworkAssignments == null)
            {
                return NotFound();
            }

            var homework_assignments = await _context.HomeworkAssignments
                .Include(h => h.Subject)
                .Include(h => h.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homework_assignments == null)
            {
                return NotFound();
            }

            return View(homework_assignments);
        }

        // GET: HomeworkAssignments/Create
        public IActionResult Create()
        {
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id");
            ViewData["TeacherId"] = new SelectList(_context.Subjects, "Id", "Id");
            return View();
        }

        // POST: HomeworkAssignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SubjectId,TeacherId,DueDate,Title")] Homework_assignments homework_assignments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homework_assignments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", homework_assignments.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Subjects, "Id", "Id", homework_assignments.TeacherId);
            return View(homework_assignments);
        }

        // GET: HomeworkAssignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HomeworkAssignments == null)
            {
                return NotFound();
            }

            var homework_assignments = await _context.HomeworkAssignments.FindAsync(id);
            if (homework_assignments == null)
            {
                return NotFound();
            }
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", homework_assignments.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Subjects, "Id", "Id", homework_assignments.TeacherId);
            return View(homework_assignments);
        }

        // POST: HomeworkAssignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubjectId,TeacherId,DueDate,Title")] Homework_assignments homework_assignments)
        {
            if (id != homework_assignments.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homework_assignments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Homework_assignmentsExists(homework_assignments.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", homework_assignments.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Subjects, "Id", "Id", homework_assignments.TeacherId);
            return View(homework_assignments);
        }

        // GET: HomeworkAssignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HomeworkAssignments == null)
            {
                return NotFound();
            }

            var homework_assignments = await _context.HomeworkAssignments
                .Include(h => h.Subject)
                .Include(h => h.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homework_assignments == null)
            {
                return NotFound();
            }

            return View(homework_assignments);
        }

        // POST: HomeworkAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HomeworkAssignments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HomeworkAssignments'  is null.");
            }
            var homework_assignments = await _context.HomeworkAssignments.FindAsync(id);
            if (homework_assignments != null)
            {
                _context.HomeworkAssignments.Remove(homework_assignments);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Homework_assignmentsExists(int id)
        {
          return (_context.HomeworkAssignments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
