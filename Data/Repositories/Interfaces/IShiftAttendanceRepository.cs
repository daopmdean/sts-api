﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;

namespace Data.Repositories.Interfaces
{
    public interface IShiftAttendanceRepository :
        IBaseRepository<ShiftAttendance>
    {
        Task<PagedList<ShiftAttendanceOverview>> GetShiftAttendancesAsync(
               string username, ShiftAttendanceParams @params);

        Task<PagedList<ShiftAttendanceOverview>> GetShiftAttendancesAsync(
               int storeId, ShiftAttendanceParams @params);

        Task<IEnumerable<ShiftAttendance>> GetShiftAttendancesAsync(
            string username, DateTimeParams @params);
    }
}
