using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnoStore.WebUI.Models.Entities
{
    public class RequestStatistic
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public int Amount { get; set; }
    }
}