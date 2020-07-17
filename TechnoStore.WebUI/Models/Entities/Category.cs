using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnoStore.WebUI.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Technic> Technics { get; set; }
        public Category()
        {
            this.Technics = new List<Technic>();
        }
    }
}