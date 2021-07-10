using System.Threading.Tasks;
using Data.Models.Requests;
using Data.Models.Responses;

namespace Service.Interfaces
{
    public interface IScheduleService
    {
        Task<ScheduleResponse> ComputeSchedule(
            int weekScheduleId, int brandId);
        Task<ScheduleRequest> GetScheduleRequest(
            int weekScheduleId, int brandId);
    }
}
