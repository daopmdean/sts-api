using System.Threading.Tasks;
using Data.Models.Requests;

namespace Service.Interfaces
{
    public interface IManagerService
    {
        Task AssignStoreManager(StoreAssign brandAssign);
        Task<StaffCreate> CreateStaff(int brandId, StaffCreate info);
        Task<StoreManagerCreate> CreateStoreManager(
            int brandId, StoreManagerCreate info);
        Task<BrandManagerCreate> CreateBrandManager(
            BrandManagerCreate info);
    }
}
