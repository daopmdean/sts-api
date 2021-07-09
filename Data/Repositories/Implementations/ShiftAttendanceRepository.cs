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
    public class ShiftAttendanceRepository :
        BaseRepository<ShiftAttendance>, IShiftAttendanceRepository
    {
        private readonly IMapper _mapper;

        public ShiftAttendanceRepository(DataContext context,
            IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedList<ShiftAttendanceOverview>> GetShiftAttendancesAsync(
            string username, ShiftAttendanceParams @params)
        {
            var source = _context.ShiftAssignments
                    .Where(x => x.Username == username)
                    .Join(_context.ShiftAttendances,
                        sas => sas.Id,
                        sat => sat.ShiftAssignmentId,
                        (sas, sat) => new ShiftAttendance
                        {
                            ShiftAssignmentId = sas.Id,
                            TimeCheckIn = sat.TimeCheckIn,
                            TimeCheckOut = sat.TimeCheckOut,
                            Status = sat.Status
                        })
                    .Where(x => x.Status == Enums.Status.Active)
                    .OrderByDescending(s => s.TimeCheckIn)
                    .ProjectTo<ShiftAttendanceOverview>(_mapper.ConfigurationProvider);

            return await PagedList<ShiftAttendanceOverview>
                .CreateAsync(source, @params.PageNumber, @params.PageSize);
        }

        public async Task<IEnumerable<ShiftAttendanceOverview>> GetShiftAttendancesAsync(
            string username, WorkHoursReportParams @params)
        {
            return await _context.ShiftAssignments
                    .Join(_context.ShiftAttendances,
                        sas => sas.Id,
                        sat => sat.ShiftAssignmentId,
                        (sas, sat) => new ShiftAttendance
                        {
                            ShiftAssignmentId = sas.Id,
                            TimeCheckIn = sat.TimeCheckIn,
                            TimeCheckOut = sat.TimeCheckOut,
                            Status = sat.Status
                        })
                    .Where(x => x.Status == Enums.Status.Active)
                    .Where(s => s.TimeCheckIn >= @params.FromDate &&
                    s.TimeCheckOut <= @params.ToDate)
                    .OrderByDescending(s => s.TimeCheckIn)
                    .ProjectTo<ShiftAttendanceOverview>(_mapper.ConfigurationProvider)
                    .ToListAsync();
        }

        public async Task<PagedList<ShiftAttendanceOverview>> GetShiftAttendancesAsync(
            int storeId, ShiftAttendanceParams @params)
        {
            var source = _context.ShiftAssignments
                    .Where(x => x.StoreId == storeId)
                    .Join(_context.ShiftAttendances,
                        sas => sas.Id,
                        sat => sat.ShiftAssignmentId,
                        (sas, sat) => new ShiftAttendance
                        {
                            ShiftAssignmentId = sas.Id,
                            TimeCheckIn = sat.TimeCheckIn,
                            TimeCheckOut = sat.TimeCheckOut,
                            Status = sat.Status
                        })
                    .Where(x => x.Status == Enums.Status.Active)
                    .OrderByDescending(s => s.TimeCheckIn)
                    .ProjectTo<ShiftAttendanceOverview>(_mapper.ConfigurationProvider);

            return await PagedList<ShiftAttendanceOverview>
                .CreateAsync(source, @params.PageNumber, @params.PageSize);
        }
    }
}
