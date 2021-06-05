using System;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;

namespace Service.Interfaces
{
    public interface IStoreService
    {
        Task<PagedList<StoreOverview>> GetStores(StoreParams @params);
        Task<Store> GetStore(int id);
        Task<Store> CreateStore(StoreCreate storeCreate);
        Task UpdateStore(int id, StoreUpdate storeUpdate);
        Task DeleteStore(int id);
    }
}
