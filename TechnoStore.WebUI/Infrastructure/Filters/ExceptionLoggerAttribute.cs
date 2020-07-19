using log4net;
using System;
using System.Text;
using System.Web.Mvc;

namespace TechnoStore.WebUI.Infrastructure.Filters
{
    public class ExceptionLoggerAttribute : FilterAttribute, IExceptionFilter
    {
        private static readonly ILog log = LogManager.GetLogger("TechnoStoreLOGGER");
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                //creating log message
                var errorMessage = new StringBuilder();
                errorMessage.Append($"URL:{filterContext.HttpContext.Request.Url}");
                errorMessage.Append(Environment.NewLine);
                errorMessage.Append($"Headers:");
                var keys = filterContext.HttpContext.Request.Headers.AllKeys;
                foreach(var key in keys)
                {
                    errorMessage.Append($"\n\t{key}:{filterContext.HttpContext.Request.Headers[key]}");
                }
                errorMessage.Append(Environment.NewLine);
                errorMessage.Append($"Request method:{filterContext.HttpContext.Request.HttpMethod}");
                errorMessage.Append(Environment.NewLine);
                errorMessage.Append($"Exception with Stack Trace:");
                
                //logging error to file and database
                log.Error(errorMessage.ToString(),filterContext.Exception);

                filterContext.ExceptionHandled = true;

                filterContext.Result = new RedirectResult("~/Error/Error/Index");
            }

        }
    }
}