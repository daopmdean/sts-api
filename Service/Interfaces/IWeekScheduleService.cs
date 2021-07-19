using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Enums;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;

namespace Service.Interfaces
{
    public interface IWeekScheduleService
    {
        Task<PagedList<WeekScheduleOverview>> GetWeekSchedulesAsync(int storeId,
            WeekScheduleParams @params);
        Task<WeekSchedule> GetWeekScheduleAsync(int id);
        Task<IEnumerable<WeekSchedule>> GetWeekScheduleAsync(
            int storeId, DateTime dateStart, Status weekStatus);
        Task<WeekSchedule> CreateWeekScheduleAsync(
            WeekScheduleCreate weekSchedule);
    }
}
