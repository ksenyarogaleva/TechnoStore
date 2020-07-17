using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnoStore.WebUI.Models.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}