using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;
using Data.Repositories.Interfaces;

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
    }
}
