using System.Threading.Tasks;
using Data.Models.Responses;
using Data.Pagings;
using Service.Interfaces;

namespace Service.Implementations
{
    public class WeekScheduleDetailService : IWeekScheduleDetailService
    {
        public WeekScheduleDetailService()
        {
        }

        public Task<PagedList<WeekScheduleDetailOverview>> GetWeekScheduleDetails(int weekScheduleId, WeekScheduleDetailParams @params)
        {
            throw new System.NotImplementedException();
        }
    }
}
