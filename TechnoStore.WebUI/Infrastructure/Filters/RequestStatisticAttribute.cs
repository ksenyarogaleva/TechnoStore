using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using TechnoStore.WebUI.Infrastructure.Abstract;
using TechnoStore.WebUI.Infrastructure.Concrete;
using TechnoStore.WebUI.Models.Entities;

namespace TechnoStore.WebUI.Infrastructure.Filters
{
    public class RequestStatisticAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpRequest = filterContext.HttpContext.Request;

            var requestStatistic = new RequestStatistic()
            {
                URL = httpRequest.RawUrl,
            };
            using (var context=new EFDbContext())
            {
                ITechnicsRepository repository = new EFTechnicsRepository(context);

                //try to find out if there were any requests to this url
                var oldStatistic = repository.RequestStatistics.FirstOrDefault(r => r.URL.ToUpper() == requestStatistic.URL.ToUpper());

                if (oldStatistic != null)
                {
                    oldStatistic.Amount++;
                    repository.SaveRequest(oldStatistic);
                }
                else
                {
                    requestStatistic.Amount++;
                    repository.SaveRequest(requestStatistic);
                }
            }
                

        }
    }
}