using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnoStore.WebUI.Infrastructure.Abstract;
using TechnoStore.WebUI.Models.Pagination;

namespace TechnoStore.WebUI.Controllers
{
    public class TechnicsController : Controller
    {
        private ITechnicsRepository repository;
        private const int pageSize = 3;

        public TechnicsController(ITechnicsRepository technicsRepository)
        {
            this.repository = technicsRepository;
        }

        public ActionResult List(int pageNumber = 1, string searchString = null, string category = null)
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("List", "Admin", new { area = "Admin" });
            }


            var technics = this.repository.Technics.OrderBy(t => t.Name);

            if (!string.IsNullOrWhiteSpace(category))
            {
                technics = technics.Where(t => t.Category.Name == category).OrderBy(t => t.Name);
            }

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                technics = technics
                    .Where(t => t.Name.ToUpper()
                    .Contains(searchString.ToUpper())).OrderBy(t => t.Name);
            }


            var listModel = Pagination.GetPagedData(technics.ToList(), pageNumber, pageSize);
            listModel.SearchingString = searchString;
            listModel.CurrentCategory = category;
            return View(listModel);
        }

        public PartialViewResult Menu(string category = null,string searchString=null,int pageNumber=1)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = this.repository.Categories
                .Select(c => c.Name)
                .Distinct()
                .OrderBy(c => c);

            ViewData["searchString"] = searchString;
            ViewData["pageNumber"]=pageNumber;

            return PartialView("CategoriesMenu", categories);
        }
    }
}