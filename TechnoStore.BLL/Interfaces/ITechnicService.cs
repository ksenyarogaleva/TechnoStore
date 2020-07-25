using System.Collections.Generic;
using TechnoStore.Common.DTO;
using TechnoStore.Common.Entities;
using TechnoStore.Common.ViewModels;

namespace TechnoStore.BLL.Interfaces
{
    public interface ITechnicService:IService<TechnicDTO>
    {
        void CreateTechnic(TechnicDTO technic);
        void UpdateTechnic(TechnicDTO technic);
        void DeleteTechnic(TechnicDTO technic);
        TechnicViewModel GetTechnicsList(int pageSize, int pageNumber,List<TechnicDTO> technics);
        TechnicEditDTO GetTechnicForEdit(int id);
    }
}
