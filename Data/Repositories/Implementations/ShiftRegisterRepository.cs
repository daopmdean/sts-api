using System.Collections.Generic;
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
    public class ShiftRegisterRepository :
        BaseRepository<ShiftRegister>, IShiftRegisterRepository
    {
        private readonly IMapper _mapper;

        public ShiftRegisterRepository(DataContext context,
            IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedList<ShiftRegisterOverview>> GetShiftRegistersAsync(
            string username, ShiftRegisterParams @params)
        {
            var source = _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.Username == username)
                .Where(s => s.TimeStart >= @params.FromDate &&
                    s.TimeStart <= @params.ToDate)
                .OrderByDescending(s => s.TimeStart)
                .ProjectTo<ShiftRegisterOverview>(_mapper.ConfigurationProvider);

            return await PagedList<ShiftRegisterOverview>
                .CreateAsync(source, @params.PageNumber, @params.PageSize);
        }

        public async Task<IEnumerable<ShiftRegister>> GetShiftRegistersAsync(
            int weekScheduleId)
        {
            return await _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.WeekScheduleId == weekScheduleId)
                .OrderByDescending(s => s.TimeStart)
                .ToListAsync();
        }
    }
}
