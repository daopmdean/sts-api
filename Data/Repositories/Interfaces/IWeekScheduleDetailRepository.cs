using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Repositories.Interfaces
{
    public interface IWeekScheduleDetailRepository :
        IBaseRepository<WeekScheduleDetail>
    {
        Task<IEnumerable<WeekScheduleDetail>> GetWeekScheduleDetailsAsync(
            int weekScheduleId);
    }
}
