using System;
using Data.Entities;
using Data.Repositories.Interfaces;

namespace Data.Repositories.Implementations
{
    public class StaffScheduleDetailRepository :
        BaseRepository<StaffScheduleDetail>, IStaffScheduleDetailRepository
    {
        public StaffScheduleDetailRepository(DataContext context) : base(context)
        {
        }
    }
}
