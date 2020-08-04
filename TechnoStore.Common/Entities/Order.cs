using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnoStore.Common.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public string ClientProfileId { get; set; }
        public int TechnicId { get; set; }
        public int OrderDetailsId { get; set; }

        [ForeignKey("ClientProfileId")]
        public virtual ClientProfile ClientProfile { get; set; }

        [ForeignKey("OrderDetailsId")]
        public virtual OrderDetails OrderDetails { get; set; }

        [ForeignKey("TechnicId")]
        public virtual Technic Technic { get; set; }
    }
}
