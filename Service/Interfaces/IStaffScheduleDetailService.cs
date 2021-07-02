using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;

namespace Service.Interfaces
{
    public interface IStaffScheduleDetailService
    {
        Task<IEnumerable<StaffScheduleDetail>> GetStaffScheduleDetails(
            int weekScheduleId);
        Task<StaffScheduleDetail> GetStaffScheduleDetail(int id);
        Task<IEnumerable<StaffScheduleDetailCreate>> CreateStaffScheduleDetailAsync(
            IEnumerable<StaffScheduleDetailCreate> create);
        Task UpdateStaffScheduleDetailAsync(int id,
            StaffScheduleDetailUpdate update);
        Task DeleteStaffScheduleDetailAsync(int id);
    }
}
