using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;

namespace Service.Interfaces
{
    public interface IManagerService
    {
        Task<StaffCreate> CreateStaff(int brandId, StaffCreate info);
        Task UpdateStaff(StaffUpdate info);
        Task<StoreManagerCreate> CreateStoreManager(
            int brandId, StoreManagerCreate info);
        Task<BrandManagerCreate> CreateBrandManager(
            BrandManagerCreate info);
        Task<ShiftAssignment> PublishSchedule(
            PublishInfo create);
        Task UnpublishSchedule(
            UnpublishInfo create);
        Task<StaffReportResponse> GetStaffReport(
            string username, DateTimeParams @params);
        Task<StoreReportResponse> GetStoreReport(
            int storeId, DateTimeParams @params);
        Task CalculateWorkTime(
            int storeId, DateTimeParams @params, int timeRange);
    }
}
