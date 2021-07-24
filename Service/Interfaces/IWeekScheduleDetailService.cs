using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;

namespace Service.Interfaces
{
    public interface IWeekScheduleDetailService
    {
        Task<IEnumerable<WeekScheduleDetail>> GetWeekScheduleDetailsAsync(
            int weekScheduleId);
        Task<WeekScheduleDetail> GetWeekScheduleDetail(int id);
        Task<IEnumerable<WeekScheduleDetail>> CreateWeekScheduleDetailAsync(
            IEnumerable<WeekScheduleDetailCreate> create);
        Task UpdateWeekScheduleDetailAsync(int id,
            WeekScheduleDetailUpdate update);
        Task DeleteWeekScheduleDetailAsync(int id);
    }
}
