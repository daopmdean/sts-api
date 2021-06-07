using System.Threading.Tasks;
using Data.Models.Responses;
using Data.Pagings;

namespace Service.Interfaces
{
    public interface IWeekScheduleDetailService
    {
        Task<PagedList<WeekScheduleDetailOverview>> GetWeekScheduleDetails(
            int weekScheduleId, WeekScheduleDetailParams @params);
    }
}
