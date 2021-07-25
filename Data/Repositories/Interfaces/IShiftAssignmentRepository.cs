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
        Task<IEnumerable<ShiftAssignmentOverview>> GetShiftAssignmentsAsync(
            string username, DateTimeParams @params);

        Task<PagedList<ShiftAssignmentOverview>> GetShiftAssignmentsAsync(
            int storeId, ShiftAssignmentParams @params);
        Task<IEnumerable<ShiftAssignment>> GetShiftAssignmentsAsync(
            int weekScheduleId, DateTime fromDate);
        Task<ShiftAssignment> GetShiftAssignmentAsync(
            string username, DateTime timeRequest, int timeRange);
    }
}
