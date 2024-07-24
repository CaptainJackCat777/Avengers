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
    public class HomeworkCreationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeworkCreationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HomeworkCreation
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HomeworkCreations.Include(h => h.HomeworkAssignment).Include(h => h.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HomeworkCreation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HomeworkCreations == null)
            {
                return NotFound();
            }

            var homework_creation = await _context.HomeworkCreations
                .Include(h => h.HomeworkAssignment)
                .Include(h => h.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homework_creation == null)
            {
                return NotFound();
            }

            return View(homework_creation);
        }

        // GET: HomeworkCreation/Create
        public IActionResult Create()
        {
            ViewData["HomeworkAssignmentId"] = new SelectList(_context.HomeworkAssignments, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            return View();
        }

        // POST: HomeworkCreation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Created,LastModified,Text,FilePath,StudentId,Grade,HomeworkAssignmentId")] Homework_creation homework_creation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homework_creation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HomeworkAssignmentId"] = new SelectList(_context.HomeworkAssignments, "Id", "Id", homework_creation.HomeworkAssignmentId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", homework_creation.StudentId);
            return View(homework_creation);
        }

        // GET: HomeworkCreation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HomeworkCreations == null)
            {
                return NotFound();
            }

            var homework_creation = await _context.HomeworkCreations.FindAsync(id);
            if (homework_creation == null)
            {
                return NotFound();
            }
            ViewData["HomeworkAssignmentId"] = new SelectList(_context.HomeworkAssignments, "Id", "Id", homework_creation.HomeworkAssignmentId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", homework_creation.StudentId);
            return View(homework_creation);
        }

        // POST: HomeworkCreation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Created,LastModified,Text,FilePath,StudentId,Grade,HomeworkAssignmentId")] Homework_creation homework_creation)
        {
            if (id != homework_creation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homework_creation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Homework_creationExists(homework_creation.Id))
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
            ViewData["HomeworkAssignmentId"] = new SelectList(_context.HomeworkAssignments, "Id", "Id", homework_creation.HomeworkAssignmentId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", homework_creation.StudentId);
            return View(homework_creation);
        }

        // GET: HomeworkCreation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HomeworkCreations == null)
            {
                return NotFound();
            }

            var homework_creation = await _context.HomeworkCreations
                .Include(h => h.HomeworkAssignment)
                .Include(h => h.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homework_creation == null)
            {
                return NotFound();
            }

            return View(homework_creation);
        }

        // POST: HomeworkCreation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HomeworkCreations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HomeworkCreations'  is null.");
            }
            var homework_creation = await _context.HomeworkCreations.FindAsync(id);
            if (homework_creation != null)
            {
                _context.HomeworkCreations.Remove(homework_creation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Homework_creationExists(int id)
        {
          return (_context.HomeworkCreations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
