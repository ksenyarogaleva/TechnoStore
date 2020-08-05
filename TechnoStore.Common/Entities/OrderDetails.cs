using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnoStore.Common.Entities
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public OrderDetails()
        {
            Orders = new List<Order>();
        }
    }
}
