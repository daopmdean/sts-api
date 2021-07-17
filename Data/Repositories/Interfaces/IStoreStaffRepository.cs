using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;

namespace Data.Repositories.Interfaces
{
    public interface IStoreStaffRepository : IBaseRepository<StoreStaff>
    {
        Task<PagedList<StoreStaffOverview>> GetStaffFromStoreAsync(int storeId,
            StoreStaffParams @params);
        Task<IEnumerable<StoreStaffOverview>> GetStaffFromStoreAsync(int storeId);
        Task<PagedList<StoreStaffOverview>> GetStoresFromStaffAsync(
            string username, StoreStaffParams @params);
        Task<IEnumerable<StoreStaffOverview>> GetStoreOverviewsFromStaffAsync(
            string username);
        Task<IEnumerable<StoreStaff>> GetStoresFromStaffAsync(
            string username);
        Task<StoreStaff> GetStoreStaffAsync(int storeId, string username);
        Task<int> GetStoreId(string username);
        Task<int> GetStaffStoreId(string username);
    }
}
