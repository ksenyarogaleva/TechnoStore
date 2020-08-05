using Ninject;
using System.Linq;
using System.Web.Mvc;
using TechnoStore.BLL.Interfaces;
using TechnoStore.Common.DTO;

namespace TechnoStore.Web.Infrastructure.Filters
{
    public class RequestStatisticAttribute : ActionFilterAttribute
    {
        [Inject]
        public IRequestService requestService { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpRequest = filterContext.HttpContext.Request;

            var requestStatistic = new RequestDTO()
            {
                URL = httpRequest.RawUrl,

            };

            var oldStatistic = requestService.Find(r => r.URL.ToUpper().Equals(requestStatistic.URL.ToUpper())).FirstOrDefault();

            if (oldStatistic != null)
            {
                oldStatistic.Amount++;
                requestService.UpdateRequestStatistic(oldStatistic);
            }
            else
            {
                requestStatistic.Amount++;
                requestService.CreateRequestStatistic(requestStatistic);
            }
        }
    }
}
