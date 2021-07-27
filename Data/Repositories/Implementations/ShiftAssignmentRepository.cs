using System;
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

        public async Task<ShiftAssignment> GetShiftAssignmentAsync(
            string username, DateTime timeRequest, int timeRange, string type)
        {
            type = type.ToLower();
            if (type == "checkin")
            {
                return await _entities
                    .Where(s => timeRequest <= s.TimeStart.AddMinutes(timeRange)
                        && timeRequest >= s.TimeStart.AddMinutes(-timeRange))
                    .FirstOrDefaultAsync(s => s.Username == username);
            }
            else if (type == "checkout")
            {
                return await _entities
                    .Where(s => timeRequest <= s.TimeEnd.AddMinutes(timeRange)
                        && timeRequest >= s.TimeEnd.AddMinutes(-timeRange))
                    .FirstOrDefaultAsync(s => s.Username == username);
            }

            return null;
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
            string username, DateTimeParams @params)
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

        public override async Task CreateAsync(ShiftAssignment entity)
        {
            if (entity.TimeStart > DateTime.Now)
            {
                await _entities.AddAsync(entity);
            }
        }

        public async Task<IEnumerable<ShiftAssignment>> GetShiftAssignmentsAsync(
            int weekScheduleId, DateTime fromDate)
        {
            return await _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.WeekScheduleId == weekScheduleId)
                .Where(s => s.TimeStart > fromDate)
                .ToListAsync();
        }
    }
}
