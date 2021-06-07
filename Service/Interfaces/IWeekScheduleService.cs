using System;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;

namespace Service.Interfaces
{
    public interface IWeekScheduleSerivce
    {
        Task<PagedList<WeekScheduleOverview>> GetWeekSchedulesAsync(int storeId,
            WeekScheduleParams @params);
        Task<WeekSchedule> GetWeekScheduleAsync(int id);
        Task<WeekSchedule> CreateWeekScheduleAsync(
            WeekScheduleCreate weekSchedule);
    }
}
