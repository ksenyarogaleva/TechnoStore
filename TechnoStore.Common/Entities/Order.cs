using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoStore.Common.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public string ClientProfileId { get; set; }

        [ForeignKey("ClientProfileId")]
        public virtual ClientProfile ClientProfile { get; set; }

        public virtual ICollection<Technic> Technics { get; set; }

        public Order()
        {
            Technics = new List<Technic>();
        }
    }
}
