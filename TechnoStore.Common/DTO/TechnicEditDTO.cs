using System.Collections.Generic;
using System.Web.Mvc;

namespace TechnoStore.Common.DTO
{
    public class TechnicEditDTO
    {
        public TechnicDTO Technic { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
