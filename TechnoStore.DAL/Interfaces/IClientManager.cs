using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoStore.Common.Entities;

namespace TechnoStore.DAL.Interfaces
{
    public interface IClientManager:IDisposable
    {
        Task CreateClient(ClientProfile client);
        Task UpdateClient(ClientProfile clientProfile);
        Task<ClientProfile> FindAsync(string clientId);
        Task<IEnumerable<Order>> GetClientOrders(string clientId);

    }
}
