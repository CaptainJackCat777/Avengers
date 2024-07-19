using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Avengers.Controllers
{
    public class HomeworkCreationController : Controller
    {
        // GET: HomeworkCreationController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HomeworkCreationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeworkCreationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeworkCreationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeworkCreationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeworkCreationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeworkCreationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeworkCreationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
