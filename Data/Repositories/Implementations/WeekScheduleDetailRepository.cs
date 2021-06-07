using Data.Entities;
using Data.Repositories.Interfaces;

namespace Data.Repositories.Implementations
{
    public class WeekScheduleDetailRepository :
        BaseRepository<WeekScheduleDetail>, IWeekScheduleDetailRepository
    {
        public WeekScheduleDetailRepository(DataContext context) : base(context)
        {
        }
    }
}
