using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnoStore.WebUI.Models.Entities.Cart
{
    public class CartTechnic
    {
        public Technic Technic { get; set; }

        public int Quantity { get; set; }
    }
}