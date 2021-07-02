using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Repositories.Interfaces
{
    public interface IStaffScheduleDetailRepository :
        IBaseRepository<StaffScheduleDetail>
    {
        Task<IEnumerable<StaffScheduleDetail>> GetStaffScheduleDetailsAsync(
            int weekScheduleId);
    }
}
