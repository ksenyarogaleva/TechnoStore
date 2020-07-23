using System.Collections.Generic;
using TechnoStore.Common.DTO;
using TechnoStore.Common.Entities;
using TechnoStore.Common.ViewModels;

namespace TechnoStore.BLL.Interfaces
{
    public interface ITechnicService:IService<Technic,TechnicDTO>
    {
        void CreateTechnic(Technic technic);
        void UpdateTechnic(Technic technic);
        void DeleteTechnic(Technic technic);
        TechnicViewModel GetTechnicsList(int pageSize, int pageNumber);
    }
}
