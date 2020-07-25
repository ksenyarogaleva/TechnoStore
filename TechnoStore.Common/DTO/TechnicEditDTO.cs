using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TechnoStore.Common.DTO
{
    public class TechnicEditDTO
    {
        public TechnicDTO Technic { get; set; }


        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
