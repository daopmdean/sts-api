using Data.Entities;
using Data.Models.Responses;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IShiftScheduleResultService
    {
        Task<ShiftScheduleResult> CreateShiftScheduleResult(int weekScheduleId);
        Task CreateShiftScheduleResult(ScheduleResponse create);
        Task<ScheduleResponse> GetScheduleResult(int id);
    }
}
