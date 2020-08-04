using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnoStore.Common.Entities
{
    public class OrderDetails
    {
        [ForeignKey("Order")]
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public virtual Order Order { get; set; }
    }
}
