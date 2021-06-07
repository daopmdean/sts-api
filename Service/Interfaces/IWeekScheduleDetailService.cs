using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;

namespace Service.Interfaces
{
    public interface IWeekScheduleDetailService
    {
        Task<PagedList<WeekScheduleDetailOverview>> GetWeekScheduleDetailsAsync(
            int weekScheduleId, WeekScheduleDetailParams @params);
        Task<WeekScheduleDetail> GetWeekScheduleDetail(int id);
        Task<WeekScheduleDetail> CreateWeekScheduleDetailAsync(
            WeekScheduleDetailCreate create);
        Task UpdateWeekScheduleDetailAsync(int id,
            WeekScheduleDetailUpdate update);
        Task DeleteWeekScheduleDetailAsync(int id);
    }
}
