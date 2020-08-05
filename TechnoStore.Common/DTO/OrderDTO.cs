using System.Collections.Generic;

namespace TechnoStore.Common.DTO
{
    public class OrderDTO
    {
        public TechnicDTO Technic { get; set; }
        public OrderDetailsDTO OrderDetails { get; set; }
    }
}
