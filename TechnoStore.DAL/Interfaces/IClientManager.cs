using System;
using TechnoStore.Common.Entities;

namespace TechnoStore.DAL.Interfaces
{
    public interface IClientManager:IDisposable
    {
        void CreateClient(ClientProfile client);
    }
}
