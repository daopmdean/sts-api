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
    public class StaffScheduleDetailRepository :
        BaseRepository<StaffScheduleDetail>, IStaffScheduleDetailRepository
    {
        private readonly IMapper _mapper;

        public StaffScheduleDetailRepository(DataContext context, IMapper mapper)
            : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedList<StaffScheduleDetailOverview>> GetStaffScheduleDetailsAsync(
            int weekScheduleId, StaffScheduleDetailParams @params)
        {
            var source = _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.WeekScheduleId == weekScheduleId)
                .OrderBy(s => s.Id)
                .ProjectTo<StaffScheduleDetailOverview>(_mapper.ConfigurationProvider);

            return await PagedList<StaffScheduleDetailOverview>
                .CreateAsync(source, @params.PageNumber, @params.PageSize);
        }
    }
}
