using System.Collections.Generic;
using TechnoStore.Common.Entities;

namespace TechnoStore.Common.ViewModels
{
    public class TechnicViewModel
    {
        public IEnumerable<Technic> Technics { get; set; }
        public string CurrentCategory { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }

        public string SearchingString { get; set; }
    }
}
