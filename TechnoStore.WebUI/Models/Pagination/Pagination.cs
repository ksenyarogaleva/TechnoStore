using System;
using System.Collections.Generic;
using System.Linq;
using TechnoStore.WebUI.Models.Entities;

namespace TechnoStore.WebUI.Models.Pagination
{
    public static class Pagination
    {
        //Returns list of items that should be placed on page of number 'pageNumber'
        public static TechnicsListViewModel GetPagedData(this List<Technic> list, int pageNumber, int pageSize) 
        {
            var result = new TechnicsListViewModel();
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