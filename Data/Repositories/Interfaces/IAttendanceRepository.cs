using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Pagings;

namespace Data.Repositories.Interfaces
{
    public interface IAttendanceRepository : IBaseRepository<Attendance>
    {
        Task<IEnumerable<Attendance>> GetAttendancesAsync(
            string username, DateTimeParams @params);
        Task<IEnumerable<Attendance>> GetAttendancesAsync(
            int storeId, DateTimeParams @params);
    }
}
