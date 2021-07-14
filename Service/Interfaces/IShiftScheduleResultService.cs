﻿using Data.Entities;
using Data.Models.Responses;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IShiftScheduleResultService
    {
        Task<ShiftScheduleResult> CreateShiftScheduleResult();
        Task<ShiftScheduleResult> CheckShiftScheduleResult(long id);
        Task<ScheduleResponse> GetScheduleResult(long id);
    }
}
