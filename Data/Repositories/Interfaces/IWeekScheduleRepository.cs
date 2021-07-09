using System;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;

namespace Data.Repositories.Interfaces
{
    public interface IWeekScheduleRepository : IBaseRepository<WeekSchedule>
    {
        Task<PagedList<WeekScheduleOverview>> GetWeekSchedulesAsync(int storeId,
            WeekScheduleParams @params);
        Task<WeekSchedule> GetWeekSchedulesAsync(int storeId, DateTime dateStart);
    }
}
