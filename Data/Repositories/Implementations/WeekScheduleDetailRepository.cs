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
    public class WeekScheduleDetailRepository :
        BaseRepository<WeekScheduleDetail>, IWeekScheduleDetailRepository
    {
        private readonly IMapper _mapper;

        public WeekScheduleDetailRepository(DataContext context, IMapper mapper)
            : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedList<WeekScheduleDetailOverview>> GetWeekScheduleDetailsAsync(
            int weekScheduleId, WeekScheduleDetailParams @params)
        {
            var source = _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.WeekScheduleId == weekScheduleId)
                .OrderBy(s => s.Id)
                .ProjectTo<WeekScheduleDetailOverview>(_mapper.ConfigurationProvider);

            return await PagedList<WeekScheduleDetailOverview>
                .CreateAsync(source, @params.PageNumber, @params.PageSize);
        }
    }
}
