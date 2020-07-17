using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnoStore.WebUI.Models.Entities;

namespace TechnoStore.WebUI.Models
{
    public class TechnicsListViewModel
    {
        public IEnumerable<Technic> Technics { get; set; }
        public string CurrentCategory { get; set; }
    }
}