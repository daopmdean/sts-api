using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;

namespace Service.Interfaces
{
    public interface IStaffScheduleDetailService
    {
        Task<PagedList<StaffScheduleDetailOverview>> GetStaffScheduleDetails(
            int weekScheduleId, StaffScheduleDetailParams @params);
        Task<StaffScheduleDetail> GetStaffScheduleDetail(int id);
        Task<IEnumerable<StaffScheduleDetailCreate>> CreateStaffScheduleDetailAsync(
            IEnumerable<StaffScheduleDetailCreate> create);
        Task UpdateStaffScheduleDetailAsync(int id,
            StaffScheduleDetailUpdate update);
        Task DeleteStaffScheduleDetailAsync(int id);
    }
}
