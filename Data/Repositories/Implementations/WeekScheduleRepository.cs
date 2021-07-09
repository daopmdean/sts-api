using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Entities;
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

            if (result?.Status == Enums.Status.Active)
                return result;

            return null;
        }

        public async Task<PagedList<WeekScheduleOverview>> GetWeekSchedulesAsync(
            int storeId, WeekScheduleParams @params)
        {
            var source = _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.StoreId == storeId)
                .OrderByDescending(ws => ws.DateStart)
                .ProjectTo<WeekScheduleOverview>(_mapper.ConfigurationProvider);

            return await PagedList<WeekScheduleOverview>
                .CreateAsync(source, @params.PageNumber, @params.PageSize);
        }

        public Task<WeekSchedule> GetWeekSchedulesAsync(int storeId, DateTime dateStart)
        {
            DayOfWeek currentDayOfWeek = dateStart.DayOfWeek;
            switch (currentDayOfWeek)
            {
                case DayOfWeek.Monday:
                    dateStart = dateStart.AddDays(0);
                    break;
                case DayOfWeek.Tuesday:
                    dateStart = dateStart.AddDays(-1);
                    break;
                case DayOfWeek.Wednesday:
                    dateStart = dateStart.AddDays(-2);
                    break;
                case DayOfWeek.Thursday:
                    dateStart = dateStart.AddDays(-3);
                    break;
                case DayOfWeek.Friday:
                    dateStart = dateStart.AddDays(-4);
                    break;
                case DayOfWeek.Saturday:
                    dateStart = dateStart.AddDays(-5);
                    break;
                case DayOfWeek.Sunday:
                    dateStart = dateStart.AddDays(-6);
                    break;
            }
            return _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.DateStart.Year == dateStart.Year
                    && s.DateStart.Month == dateStart.Month
                    && s.DateStart.Day == dateStart.Day)
                .Where(w => w.StoreId == storeId)
                .FirstOrDefaultAsync();
        }
    }
}
