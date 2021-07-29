using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;

namespace Service.Interfaces
{
    public interface IManagerService
    {
        Task<StaffCreate> CreateStaff(int brandId, StaffCreate info);
        Task<StoreManagerCreate> CreateStoreManager(
            int brandId, StoreManagerCreate info);
        Task<BrandManagerCreate> CreateBrandManager(
            BrandManagerCreate info);
        Task<ShiftAssignment> PublishSchedule(
            PublishInfo create);
        Task UnpublishSchedule(
            UnpublishInfo create);
    }
}
