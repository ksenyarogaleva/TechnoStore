using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TechnoStore.WebUI.Infrastructure.Abstract;
using TechnoStore.WebUI.Models.Entities;

namespace TechnoStore.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private ITechnicsRepository repository;
        public AdminController(ITechnicsRepository technicsRepository)
        {
            this.repository = technicsRepository;
        }

        public ActionResult List()
        {
            var technicsList = this.repository.Technics.Include(t => t.Category);
            return View(technicsList);
        }

        public ActionResult Create()
        {
            return View("Edit", new Technics());
        }

        public ActionResult Edit(int technicsId)
        {
            var product = this.repository.Technics.Include(t => t.Category).FirstOrDefault(p => p.TechnicsId == technicsId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Technics technics)
        {
            if (ModelState.IsValid)
            {
                this.repository.SaveTechnics(technics);

                TempData["message"] = string.Format("Changes in product \"{0}\" were saved.", technics.Name);
                return RedirectToAction("List");
            }
            else
            {
                return View(technics);
            }
        }

        [HttpPost]
        public ActionResult Delete(int technicsId)
        {
            var deletedTechnics = this.repository.DeleteTechnics(technicsId);
            if (deletedTechnics != null)
            {
                TempData["message"] = string.Format("Product \"{0}\" was deleted.", deletedTechnics.Name);
            }

            return RedirectToAction("List");
        }
    }
}