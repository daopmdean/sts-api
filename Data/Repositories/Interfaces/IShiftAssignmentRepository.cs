using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;

namespace Data.Repositories.Interfaces
{
    public interface IShiftAssignmentRepository :
        IBaseRepository<ShiftAssignment>
    {
        Task<PagedList<ShiftAssignmentOverview>> GetShiftAssignmentsAsync(
            string username, ShiftAssignmentParams @params);
        Task<PagedList<ShiftAssignmentOverview>> GetShiftAssignmentsAsync(
            int storeId, ShiftAssignmentParams @params);
        Task<IEnumerable<ShiftAssignmentOverview>> GetShiftAssignmentsAsync(
            string username, WorkHoursReportParams @params);
    }
}
