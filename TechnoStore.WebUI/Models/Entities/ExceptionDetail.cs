using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace TechnoStore.WebUI.Models.Entities
{
    public class ExceptionDetail
    {
        public string URL { get; set; }
        public NameValueCollection Headers { get; set; }
        public string RequestMethod { get; set; }
        public string ExceptionName { get; set; }
        public  string StackTrace { get; set; }
    }
}