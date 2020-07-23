using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoStore.Common.DTO
{
    public class TechnicDTO
    {
        public int Id { get; set; }

        [Display(Name="Name")]
        [Required(ErrorMessage = "Please, enter product name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name ="Description")]
        public string Description { get; set; }

        [Display(Name ="Price (USD)")]
        [DisplayFormat(DataFormatString = "{0:#.##}")]
        [Required]
        [Range(0.01,double.MaxValue,ErrorMessage ="Please, enter product price more than 0")]
        public decimal Price { get; set; }
        
        public string Category { get; set; }
    }
}
