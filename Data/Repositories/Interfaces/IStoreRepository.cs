using System;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;

namespace Data.Repositories.Interfaces
{
    public interface IStoreRepository : IBaseRepository<Store>
    {
        Task<PagedList<StoreOverview>> GetStoresAsync(StoreParams @params);
    }
}
