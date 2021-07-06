using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class WeekScheduleDetailRepository :
        BaseRepository<WeekScheduleDetail>, IWeekScheduleDetailRepository
    {
        public WeekScheduleDetailRepository(DataContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<WeekScheduleDetail>> GetWeekScheduleDetailsAsync(
            int weekScheduleId)
        {
            return await _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.WeekScheduleId == weekScheduleId)
                .OrderBy(s => s.WorkStart)
                .ToListAsync();
        }
    }
}
