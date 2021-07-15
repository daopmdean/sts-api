using Data.Entities;
using Data.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IShiftScheduleDetailResultRepository :
        IBaseRepository<ShiftScheduleDetailResult>
    {
        Task<IEnumerable<ShiftAssignmentResponse>> GetShiftAssignments(
            long shiftScheduleResultId);
    }
}
