using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TechnoStore.WebUI.Infrastructure.Concrete;
using TechnoStore.WebUI.Models.Entities;

namespace TechnoStore.WebUI.Infrastructure.Filters
{
    public class ExceptionLoggerAttribute : FilterAttribute, IExceptionFilter
    {
        private static readonly ILog log = LogManager.GetLogger("TechnoStoreLOGGER");
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                var exceptionDetail = new ExceptionDetail()
                {
                    URL = filterContext.HttpContext.Request.Url.ToString(),
                    Headers = new System.Collections.Specialized.NameValueCollection(filterContext.HttpContext.Request.Headers),
                    ExceptionName = filterContext.Exception.Message,
                    RequestMethod = filterContext.HttpContext.Request.HttpMethod,
                    StackTrace = filterContext.Exception.StackTrace,
                };

                var errorMessage = new StringBuilder();
                errorMessage.Append($"URL:{exceptionDetail.URL}");
                errorMessage.Append(Environment.NewLine);
                errorMessage.Append($"Headers:");
                var keys = exceptionDetail.Headers.AllKeys;
                foreach(var key in keys)
                {
                    errorMessage.Append($"\n\t{key}:{exceptionDetail.Headers[key]}");
                }
                errorMessage.Append(Environment.NewLine);
                errorMessage.Append($"Request method:{exceptionDetail.RequestMethod}");
                errorMessage.Append(Environment.NewLine);
                errorMessage.Append($"Exception with Stack Trace:");
                log.Error(errorMessage.ToString(),filterContext.Exception);

                filterContext.ExceptionHandled = true;

                filterContext.Result = new RedirectResult("Error/Error/Index");





            }

        }
    }
}