using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using TechnoStore.BLL.Interfaces;
using TechnoStore.Common.DTO;

namespace TechnoStore.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ITechnicService technicService;
        private ICategoryService categoryService;
        public AdminController(ITechnicService technicService,ICategoryService categoryService)
        {
            this.technicService = technicService;
            this.categoryService = categoryService;
        }

        public ActionResult List(string searchString, string categoryName)
        {
            var categoryList = new List<string>();

            var categoryQuery = from d in categoryService.GetAll()
                                orderby d.Name
                                select d.Name;

            categoryList.AddRange(categoryQuery);
            ViewBag.categoryName = new SelectList(categoryList);


            var technicsList = this.technicService.GetAll();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                technicsList = technicsList
                    .Where(t => t.Name.ToUpper()
                    .Contains(searchString.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                technicsList = technicsList.Where(t => t.Category == categoryName);
            }


            return View(technicsList);
        }

        public ActionResult Create()
        {
            return RedirectToAction("Edit");
        }

        public ActionResult Edit(int technicsId = 0)
        {
            var product = this.technicService.GetTechnicForEdit(technicsId);
            
            product.Categories = new SelectList(this.categoryService.GetAll(), "Id", "Name");

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(TechnicEditDTO technic)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dto = technic.Technic;
                    dto.Category = this.categoryService.GetSingle(technic.CategoryId).Name;
                    this.technicService.UpdateTechnic(dto);

                    TempData["message"] = string.Format("Changes in product \"{0}\" were saved.", technic.Technic.Name);
                    return RedirectToAction("List");
                }
            }
            catch (RetryLimitExceededException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            technic.Categories = new SelectList(this.categoryService.GetAll(), "Id", "Name");
            return View(technic);
        }

        [HttpPost]
        public ActionResult Delete(int technicsId)
        {
            var deletedTechnic = this.technicService.GetSingle(technicsId);
            this.technicService.DeleteTechnic(deletedTechnic);
                TempData["message"] = string.Format("Product \"{0}\" was deleted.", deletedTechnic.Name);

            return RedirectToAction("List");
        }

        public ActionResult ShowProfile()
        {
            //var totalRequestsAmount = 0;
            //foreach(var request in this.repository.RequestStatistics)
            //{
            //    totalRequestsAmount += request.Amount;
            //}
            //ViewBag.TotalAmount = totalRequestsAmount;
            //return View("Profile",this.repository.RequestStatistics);
            return Content("");
        }
    }


   
}
