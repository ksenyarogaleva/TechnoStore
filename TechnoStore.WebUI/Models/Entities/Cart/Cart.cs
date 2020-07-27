using System.Collections.Generic;

namespace TechnoStore.WebUI.Models.Entities.Cart
{
    public class Cart
    {
        public IEnumerable<CartTechnic> TechnicsInCart { get; private set; }
    }
}