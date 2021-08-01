using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class StoreScheduleDetailRepository :
        BaseRepository<StoreScheduleDetail>, IStoreScheduleDetailRepository
    {
        public StoreScheduleDetailRepository(DataContext context)
            : base(context) { }

        public async Task<IEnumerable<StoreScheduleDetail>> GetStoreScheduleDetailsAsync(
            int weekScheduleId)
        {
            var source = await _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.WeekScheduleId == weekScheduleId)
                .ToListAsync();

            return source;
        }
    }
}
