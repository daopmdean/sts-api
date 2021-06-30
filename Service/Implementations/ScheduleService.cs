using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Service.Interfaces;

namespace Service.Implementations
{
    public class ScheduleService : IScheduleService
    {
        public ScheduleService()
        {
        }

        public Task<IEnumerable<ShiftAssignment>> ComputeSchedule(
            ScheduleRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
