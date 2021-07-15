using Data.Entities;
using Data.Models.Requests;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IScheduleService
    {
        Task<ShiftScheduleResult> ComputeSchedule(
            int weekScheduleId, int brandId);
        Task<ScheduleRequest> GetScheduleRequest(
            int weekScheduleId, int brandId);
    }
}
