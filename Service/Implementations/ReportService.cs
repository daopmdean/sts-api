using System;
using System.Threading.Tasks;
using Data.Models.Responses;
using Data.Pagings;
using Data.Repositories.Interfaces;
using Service.Interfaces;

namespace Service.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IShiftAssignmentRepository _shiftAssignmentRepo;
        private readonly IShiftAttendanceRepository _shiftAttendanceRepo;

        public ReportService(
            IShiftAssignmentRepository shiftAssignmentRepo,
            IShiftAttendanceRepository shiftAttendanceRepo)
        {
            _shiftAssignmentRepo = shiftAssignmentRepo;
            _shiftAttendanceRepo = shiftAttendanceRepo;
        }

        public async Task<WorkHoursResponse> GetWorkHoursResponse(
            string username, DateTimeParams @params)
        {
            WorkHoursResponse result = new();
            var shiftAssignments = await _shiftAssignmentRepo
                .GetShiftAssignmentsAsync(username, @params);

            var shiftAttendances = await _shiftAttendanceRepo
                .GetShiftAttendancesAsync(username, @params);

            foreach (var shiftAssignment in shiftAssignments)
            {
                TimeSpan timeSpan =
                    shiftAssignment.TimeEnd - shiftAssignment.TimeStart;
                result.HoursAssigned += timeSpan.TotalHours;
            }

            foreach (var shiftAttendance in shiftAttendances)
            {
                TimeSpan timeSpan =
                    shiftAttendance.TimeCheckOut - shiftAttendance.TimeCheckIn;
                result.HoursAssigned += timeSpan.TotalHours;
            }

            return result;
        }
    }
}
