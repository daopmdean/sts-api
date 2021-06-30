using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Repositories.Interfaces
{
    public interface IStoreScheduleDetailRepository :
        IBaseRepository<StoreScheduleDetail>
    {
        Task<IEnumerable<StoreScheduleDetail>> GetStoreScheduleDetailsAsync(
            int weekScheduleId);
    }
}
