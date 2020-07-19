using System.Web.Mvc;
using TechnoStore.WebUI.Infrastructure.Filters;

namespace TechnoStore.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionLoggerAttribute());
        }
    }
}