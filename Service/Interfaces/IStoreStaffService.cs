using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;

namespace Service.Interfaces
{
    public interface IStoreStaffService
    {
        Task<PagedList<StoreStaffOverview>> GetStoresFromStaffAsync(
            string username, StoreStaffParams @params);
        Task<PagedList<StoreStaffOverview>> GetStaffFromStoreAsync(
            int storeId, StoreStaffParams @params);
        Task<StoreStaff> GetStoreStaffAsync(int storeId, string username);
        Task<int> GetStaffStoreIdAsync(string username);
        Task<StoreStaff> CreateStoreStaff(StoreStaffCreate create);
        Task<StoreStaff> AssignStaff(StoreAssign assign);
        Task<StoreStaff> AssignStoreManager(StoreAssign assign);
        Task UpdateStoreStaff(int storeId, string username,
            StoreStaffUpdate update);
        Task DeleteStoreStaff(int storeId, string username);
    }
}
