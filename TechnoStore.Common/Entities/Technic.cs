using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnoStore.Common.Entities
{
    public class Technic
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

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public Technic()
        {
            Orders = new List<Order>();
        }
    }
}
