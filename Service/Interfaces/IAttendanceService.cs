using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Data.Pagings;

namespace Service.Interfaces
{
    public interface IAttendanceService
    {
        Task<Attendance> GetAttendanceAsync(int id);
        Task<IEnumerable<Attendance>> GetAttendancesAsync(
            string username, DateTimeParams @params);
        Task<IEnumerable<Attendance>> GetAttendancesAsync(
            int storeId, DateTimeParams @params);
        Task<Attendance> CreateAttendanceAsync(AttendanceCreate create);
        Task<Attendance> CreateAttendanceManualAsync(
            string username, AttendanceManualCreate create);
        Task UpdateAttendanceAsync(int id, AttendanceUpdate update);
        Task DeleteAttendanceAsync(int id);
    }
}
