using System;
using System.Threading.Tasks;
using Data.Models.Responses;
using Data.Pagings;
using Data.Repositories.Interfaces;
using Service.Interfaces;

namespace Service.Implementations
{
    public class StaffScheduleDetailService : IStaffScheduleDetailService
    {
        private readonly IStaffScheduleDetailRepository _repository;

        public StaffScheduleDetailService(IStaffScheduleDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedList<StaffScheduleDetailOverview>> GetStaffScheduleDetails(
            int weekScheduleId, StaffScheduleDetailParams @params)
        {
            return await _repository
                .GetStaffScheduleDetailsAsync(weekScheduleId, @params);
        }
    }
}
