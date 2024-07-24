using Avengers.Data;
using Avengers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace Avengers.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ProfileModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Users User { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            User = await _context.Users.FindAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            return Page();
        }

        public string GetRandomColor()
        {
            var random = new Random();
            var colors = new[] { "#FF5733", "#33FF57", "#3357FF", "#FF33A2", "#F7FF33" };
            return colors[random.Next(colors.Length)];
        }
    }
}
