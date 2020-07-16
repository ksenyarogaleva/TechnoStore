using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnoStore.WebUI.Models.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public ICollection<Technics> Technics { get; set; }
        public Category()
        {
            this.Technics = new List<Technics>();
        }
    }
}