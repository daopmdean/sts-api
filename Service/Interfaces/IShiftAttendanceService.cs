using System;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;

namespace Service.Interfaces
{
    public interface IShiftAttendanceService
    {
        Task<PagedList<ShiftAttendanceOverview>> GetShiftAttendencesAsync(
            string username, ShiftAttendanceParams @params);
        Task<PagedList<ShiftAttendanceOverview>> GetShiftAttendencesAsync(
            int StoreId, ShiftAttendanceParams @params);
        Task<ShiftAttendance> GetShiftAttendance(int id);
        Task<ShiftAttendance> CreateShiftAttendance(
            ShiftAttendanceCreate create, int timeRange);
        Task UpdateShiftAttendance(int id, ShiftAttendanceUpdate update);
        Task DeleteShiftAttendance(int id);
    }
}
