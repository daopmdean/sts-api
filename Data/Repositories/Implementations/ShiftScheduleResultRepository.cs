using Data.Entities;
using Data.Repositories.Interfaces;

namespace Data.Repositories.Implementations
{
    public class ShiftScheduleResultRepository : BaseRepository<ShiftScheduleResult>,
        IShiftScheduleResultRepository
    {
        public ShiftScheduleResultRepository(DataContext context) : base(context)
        {

        }
    }
}
