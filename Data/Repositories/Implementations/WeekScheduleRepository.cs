using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Entities;
using Data.Enums;
using Data.Models.Responses;
using Data.Pagings;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class WeekScheduleRepository : BaseRepository<WeekSchedule>,
        IWeekScheduleRepository
    {
        private readonly IMapper _mapper;

        public WeekScheduleRepository(DataContext context,
            IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public override async Task<WeekSchedule> GetByIdAsync(int id)
        {
            var result = await _entities
                .Include(x => x.WeekScheduleDetails)
                .Include(x => x.StaffScheduleDetails)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result?.Status == Status.Active)
                return result;

            return null;
        }

        public async Task<PagedList<WeekScheduleOverview>> GetWeekSchedulesAsync(
            int storeId, WeekScheduleParams @params)
        {
            var source = _entities
                .Where(s => s.Status == Status.Active)
                .Where(s => s.StoreId == storeId)
                .OrderByDescending(ws => ws.DateStart)
                .ProjectTo<WeekScheduleOverview>(_mapper.ConfigurationProvider);

            return await PagedList<WeekScheduleOverview>
                .CreateAsync(source, @params.PageNumber, @params.PageSize);
        }

        public Task<WeekSchedule> GetWeekSchedulesAsync(
            int storeId, DateTime dateStart)
        {
            Helpers.Helper.TransformDateStart(ref dateStart);
            return _entities
                .Where(s => s.Status == Status.Active)
                .Where(s => s.DateStart.Year == dateStart.Year
                    && s.DateStart.Month == dateStart.Month
                    && s.DateStart.Day == dateStart.Day)
                .Where(w => w.StoreId == storeId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<WeekSchedule>> GetWeekSchedulesAsync(
            int storeId, DateTime dateStart, Status weekStatus)
        {
            Helpers.Helper.TransformDateStart(ref dateStart);
            return await _entities
                .Where(s => s.Status == weekStatus)
                .Where(s => s.DateStart.Year == dateStart.Year
                    && s.DateStart.Month == dateStart.Month
                    && s.DateStart.Day == dateStart.Day)
                .Where(w => w.StoreId == storeId)
                .ToListAsync();
        }
    }
}
