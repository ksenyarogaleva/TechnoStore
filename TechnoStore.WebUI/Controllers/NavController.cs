using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnoStore.WebUI.Infrastructure.Abstract;

namespace TechnoStore.WebUI.Controllers
{
    //Navigation controller
    public class NavController : Controller
    {
        private ITechnicsRepository repository;

        public NavController(ITechnicsRepository technicsRepository)
        {
            this.repository = technicsRepository;
        }

        public PartialViewResult Menu(string category=null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = this.repository.Categories
                .Select(c => c.Name)
                .Distinct()
                .OrderBy(c => c);

            return PartialView("CategoriesMenu",categories);
        }
    }
}