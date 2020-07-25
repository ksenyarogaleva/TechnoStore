using System;
using System.Collections.Generic;
using System.Linq;
using TechnoStore.Common.DTO;
using TechnoStore.Common.ViewModels;

namespace TechnoStore.Common.Infrastructure
{
    public static class TechnicsPageDetails
    {
        //Returns list of items that should be placed on page of number 'pageNumber'
        public static TechnicViewModel GetPagedData(this List<TechnicDTO> list, int pageNumber, int pageSize)
        {
            var result = new TechnicViewModel();
            result.Technics = list
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            result.CurrentPage = pageNumber;
            result.TotalPages = Convert.ToInt32(Math.Ceiling((double)list.Count() / pageSize));

            return result;
        }
    }
}
