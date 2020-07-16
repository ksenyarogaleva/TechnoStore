using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TechnoStore.WebUI.Models.Entities
{
    public class Technics
    {
        [Key]
        [HiddenInput(DisplayValue =false)]
        public int TechnicsId { get; set; }

        [Display(Name="Name")]
        [Required(ErrorMessage = "Please, enter product name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name ="Description")]
        public string Description { get; set; }

        [Display(Name ="Price (USD)")]
        [Required]
        [Range(0.01,double.MaxValue,ErrorMessage ="Please, enter product price more than 0")]
        public decimal Price { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? CategoryId { get; set; }

        [Display(Name ="Category")]
        [Required(ErrorMessage = "Please, enter product category")]
        public Category Category { get; set; }
    }
}
