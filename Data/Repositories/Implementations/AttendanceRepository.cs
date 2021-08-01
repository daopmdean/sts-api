using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Data.Pagings;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class AttendanceRepository :
        BaseRepository<Attendance>, IAttendanceRepository
    {
        public AttendanceRepository(DataContext context)
            : base(context) { }

        public async Task<IEnumerable<Attendance>> GetAttendancesAsync(
            string username, DateTimeParams @params)
        {
            return await _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.Username == username)
                .Where(s => s.TimeCheck >= @params.FromDate
                    && s.TimeCheck <= @params.ToDate)
                .OrderByDescending(s => s.TimeCheck)
                .ToListAsync();
        }

        public async Task<IEnumerable<Attendance>> GetAttendancesAsync(
            int storeId, DateTimeParams @params)
        {
            return await _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.StoreId == storeId)
                .Where(s => s.TimeCheck >= @params.FromDate
                    && s.TimeCheck <= @params.ToDate)
                .OrderByDescending(s => s.TimeCheck)
                .ToListAsync();
        }
    }
}
