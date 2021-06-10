using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;

namespace Data.Repositories.Interfaces
{
    public interface IShiftAttendanceRepository :
        IBaseRepository<ShiftAttendance>
    {
        Task<PagedList<ShiftAttendanceOverview>> GetShiftAttendancesAsync(
               string username, ShiftAttendanceParams @params);
    }
}
