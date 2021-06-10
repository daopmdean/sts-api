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
                    .Join(_context.ShiftAttendances,
                        sas => sas.Id,
                        sat => sat.ShiftAssignmentId,
                        (sas, sat) => new
                        {
                            Id = sat.Id,
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
