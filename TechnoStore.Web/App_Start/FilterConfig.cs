using System.Web.Mvc;
using TechnoStore.Web.Infrastructure.Filters;

namespace TechnoStore.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionLoggerAttribute());
        }
    }
}