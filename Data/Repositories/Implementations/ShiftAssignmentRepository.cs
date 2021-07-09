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
    public class ShiftAssignmentRepository :
        BaseRepository<ShiftAssignment>,
        IShiftAssignmentRepository
    {
        private readonly IMapper _mapper;

        public ShiftAssignmentRepository(DataContext context,
            IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedList<ShiftAssignmentOverview>> GetShiftAssignmentsAsync(
            string username, ShiftAssignmentParams @params)
        {
            var source = _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.Username == username)
                .Where(s => s.TimeStart >= @params.FromDate &&
                    s.TimeStart <= @params.ToDate.AddDays(1))
                .OrderByDescending(s => s.TimeStart)
                .ProjectTo<ShiftAssignmentOverview>(_mapper.ConfigurationProvider);

            return await PagedList<ShiftAssignmentOverview>
                .CreateAsync(source, @params.PageNumber, @params.PageSize);
        }

        public async Task<PagedList<ShiftAssignmentOverview>> GetShiftAssignmentsAsync(
            int storeId, ShiftAssignmentParams @params)
        {
            var source = _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.StoreId == storeId)
                .OrderByDescending(s => s.TimeStart)
                .ProjectTo<ShiftAssignmentOverview>(_mapper.ConfigurationProvider);

            return await PagedList<ShiftAssignmentOverview>
                .CreateAsync(source, @params.PageNumber, @params.PageSize);
        }

        public async Task<IEnumerable<ShiftAssignmentOverview>> GetShiftAssignmentsAsync(
            string username, WorkHoursReportParams @params)
        {
            return await _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.Username == username)
                .Where(s => s.TimeStart >= @params.FromDate &&
                    s.TimeEnd <= @params.ToDate.AddDays(1))
                .OrderByDescending(s => s.TimeStart)
                .ProjectTo<ShiftAssignmentOverview>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
