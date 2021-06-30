using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;

namespace Service.Interfaces
{
    public interface IScheduleService
    {
        Task<IEnumerable<ShiftAssignment>> ComputeSchedule(
            ScheduleRequest request);
    }
}
