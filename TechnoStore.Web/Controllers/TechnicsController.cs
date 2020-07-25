using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TechnoStore.BLL.Interfaces;
using TechnoStore.Common.Infrastructure;

namespace TechnoStore.Web.Controllers
{
    
    public class TechnicsController : Controller
    {
        private ITechnicService technicService;
        private ICategoryService categoryService;
        private const int pageSize = 3;

        public TechnicsController(ITechnicService technicService, ICategoryService categoryService)
        {
            this.technicService = technicService;
            this.categoryService = categoryService;
        }

        public ActionResult List(int pageNumber = 1, string searchString = null, string category = null)
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("List", "Admin", new { area = "Admin" });
            }

            var technics = this.technicService.GetAll();

            if (!string.IsNullOrWhiteSpace(category))
            {
                technics = technics.Where(t => t.Category == category).OrderBy(t => t.Name);
            }

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                technics = technics
                    .Where(t => t.Name.ToUpper()
                    .Contains(searchString.ToUpper())).OrderBy(t => t.Name);
            }


            var viewModel = technics.ToList().GetPagedData(pageNumber, pageSize);
            viewModel.SearchingString = searchString;
            viewModel.CurrentCategory = category;
            return View(viewModel);
        }

        public PartialViewResult Menu(string category = null,string searchString=null,int pageNumber=1)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = this.categoryService
                .GetAll()
                .Select(c => c.Name)
                .Distinct()
                .OrderBy(c => c);

            ViewData["searchString"] = searchString;
            ViewData["pageNumber"]=pageNumber;

            return PartialView("CategoriesMenu", categories);
        }

    }
}