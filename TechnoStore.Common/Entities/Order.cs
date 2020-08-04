using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnoStore.Common.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public decimal TotalSum { get; set; }

        public string ClientProfileId { get; set; }

        [ForeignKey("ClientProfileId")]
        public virtual ClientProfile ClientProfile { get; set; }
        public virtual OrderDetails OrderDetails { get; set; }


        public virtual ICollection<Technic> Technics { get; set; }

        public Order()
        {
            Technics = new List<Technic>();
        }
    }
}
