using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;

namespace Data.Repositories.Interfaces
{
    public interface IWeekScheduleDetailRepository :
        IBaseRepository<WeekScheduleDetail>
    {
        Task<PagedList<WeekScheduleDetailOverview>> GetWeekScheduleDetailsAsync(
            int weekScheduleId, WeekScheduleDetailParams @params);
    }
}
