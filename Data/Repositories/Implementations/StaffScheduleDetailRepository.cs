using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class StaffScheduleDetailRepository :
        BaseRepository<StaffScheduleDetail>, IStaffScheduleDetailRepository
    {
        public StaffScheduleDetailRepository(DataContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<StaffScheduleDetail>> GetStaffScheduleDetailsAsync(
            int weekScheduleId)
        {
            return await _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.WeekScheduleId == weekScheduleId)
                .OrderBy(s => s.Id)
                .ToListAsync();
        }
    }
}
