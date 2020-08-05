using TechnoStore.Common.DTO;

namespace TechnoStore.BLL.Interfaces
{
    public interface IRequestService:IService<RequestDTO>
    {
        void CreateRequestStatistic(RequestDTO request);
        void UpdateRequestStatistic(RequestDTO request);
    }
}
