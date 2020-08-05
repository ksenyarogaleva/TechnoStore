using System.ComponentModel.DataAnnotations;

namespace TechnoStore.Common.DTO
{
    public class OrderDetailsDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please,enter the country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please,enter the city")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please,enter shipping address")]
        public string Address { get; set; }

    }
}
