using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Avengers.Models;
using System.Collections.Generic;
using System.Linq;

namespace Avengers.Controllers
{
    public class HomeworkAssignmentsController : Controller
    {
        private static List<Homework_assignments> assignments = new List<Homework_assignments>
        {
            // Sample data, can be removed later
            new Homework_assignments { Id = 1, Title = "Math Homework", DueDate = "2024-07-20", Grade = Grade.VeryGoodMark }
        };

        // GET: HomeworkAssignmentsController
        public ActionResult Index()
        {
            if (assignments == null || !assignments.Any())
            {
                ViewBag.Message = "No homework assignments available.";
            }
            return View("Homework_assignments", assignments);
        }

        // Other actions can remain as is
    }
}
