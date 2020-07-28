using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TechnoStore.Common.DTO
{
    public class TechnicEditDTO
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please, enter product name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Price (USD)")]
        [DisplayFormat(DataFormatString = "{0:#.##}")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please, enter product price more than 0")]
        public decimal Price { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }

        [HiddenInput]
        public string Category { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
