using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoStore.Common.Entities;

namespace TechnoStore.Common.DTO
{
    public class OrderDTO
    {
        public List<TechnicDTO> Technics { get; set; }
        public OrderDetailsDTO OrderDetails { get; set; }

        
    }
}
