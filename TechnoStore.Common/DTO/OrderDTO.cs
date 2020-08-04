using System.Collections.Generic;

namespace TechnoStore.Common.DTO
{
    public class OrderDTO
    {
        public List<TechnicDTO> Technics { get; set; }
        public decimal TotalSum { get; set; }
        public OrderDetailsDTO OrderDetails { get; set; }
    }
}
