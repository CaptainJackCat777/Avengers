using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Avengers.Data;
using Avengers.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avengers.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<HomeworkAssignments> HomeworkAssignments { get; set; }

        public string Message { get; set; }

        public async Task OnGetAsync()
        {
            HomeworkAssignments = await _context.HomeworkAssignments
                .Include(h => h.Teacher)
                .Include(h => h.Subject)
                .ToListAsync();

            if (HomeworkAssignments == null || HomeworkAssignments.Count == 0)
            {
                Message = "No assignments found.";
            }
        }
    }
}
