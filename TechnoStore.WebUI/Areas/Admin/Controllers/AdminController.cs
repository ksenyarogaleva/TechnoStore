using Ninject.Infrastructure.Language;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using TechnoStore.WebUI.Infrastructure.Abstract;
using TechnoStore.WebUI.Models.Entities;

namespace TechnoStore.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ITechnicsRepository repository;
        public AdminController(ITechnicsRepository technicsRepository)
        {
            this.repository = technicsRepository;
        }

        public ActionResult List(string searchString, string categoryName)
        {
            var categoryList = new List<string>();

            var categoryQuery = from d in repository.Categories
                                orderby d.Name
                                select d.Name;

            categoryList.AddRange(categoryQuery);
            ViewBag.categoryName = new SelectList(categoryList);


            //var technicsList = this.repository.Technics.Include(t => t.Category);
            var technicsList = this.repository.Technics;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                technicsList = technicsList
                    .Where(t => t.Name.ToUpper()
                    .Contains(searchString.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                technicsList = technicsList.Where(t => t.Category.Name == categoryName);
            }


            return View(technicsList);
        }

        public ActionResult Create()
        {
            return RedirectToAction("Edit");
        }

        public ActionResult Edit(int technicsId = 0)
        {
            var product = this.repository.Technics.FirstOrDefault(t=>t.Id==technicsId);
            if (product is null)
            {
                product = new Technic();
            }
            product.CategoryList = new SelectList(this.repository.Categories, "Id", "Name");

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price,CategoryId")] Technic technics)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.repository.SaveTechnics(technics);

                    TempData["message"] = string.Format("Changes in product \"{0}\" were saved.", technics.Name);
                    return RedirectToAction("List");
                }
            }
            catch (RetryLimitExceededException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            technics.CategoryList = new SelectList(this.repository.Categories, "ID", "Name");
            return View(technics);
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
