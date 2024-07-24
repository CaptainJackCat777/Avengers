using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Avengers.Data;
using Avengers.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Avengers.Pages
{
    public class GradesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public GradesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<HomeworkAssignments> HomeworkAssignments { get; set; }

        public async Task OnGetAsync()
        {
            HomeworkAssignments = await _context.HomeworkAssignments
                .Include(h => h.Subject)
                .Include(h => h.Teacher)
                .ToListAsync();
        }
    }
}
