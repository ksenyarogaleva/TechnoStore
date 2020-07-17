using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnoStore.WebUI.Infrastructure.Abstract;
using TechnoStore.WebUI.Models;

namespace TechnoStore.WebUI.Controllers
{
    public class TechnicsController : Controller
    {
        private ITechnicsRepository repository;

        public TechnicsController(ITechnicsRepository technicsRepository)
        {
            this.repository = technicsRepository;
        }

        
        public ActionResult List(string category=null)
        {

          if (User.IsInRole("Admin"))
            {
                return RedirectToAction("List", "Admin",new { area = "Admin" });
            }

            var listModel = new TechnicsListViewModel
            {
                CurrentCategory = category

            };

            if (!string.IsNullOrEmpty(category))
            {
                listModel.Technics = this.repository.Technics
                .Where(t => t.Category.Name == category)
                .OrderBy(t => t.Name);
            }
            else
            {
                listModel.Technics = this.repository.Technics
                    .OrderBy(t => t.Name);
            }

            

            return View(listModel);
        }
    }
}