using System.Collections.Generic;
using TechnoStore.Common.DTO;

namespace TechnoStore.Common.Infrastructure
{
    public class Cart
    {
        public IEnumerable<TechnicInCart> TechnicsInCart { get; private set; }
    }
}
