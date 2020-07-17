using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechnoStore.WebUI.Models.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Please,enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please,enter the first shipping address")]
        [DisplayName("First address")]
        public string Line1 { get; set; }

        [DisplayName("Second address")]
        public string Line2 { get; set; }

        [DisplayName("Third address")]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Please,enter the city")]
        [DisplayName("City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please,enter the country")]
        [DisplayName("Country")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}