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
        Task<PagedList<StoreStaffOverview>> GetStoresFromStaffAsync(string username,
            StoreStaffParams @params);
    }
}
