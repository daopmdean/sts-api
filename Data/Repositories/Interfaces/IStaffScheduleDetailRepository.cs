using System;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;

namespace Data.Repositories.Interfaces
{
    public interface IStaffScheduleDetailRepository :
        IBaseRepository<StaffScheduleDetail>
    {
        Task<PagedList<StaffScheduleDetailOverview>> GetStaffScheduleDetailsAsync(
            int weekScheduleId, StaffScheduleDetailParams @params);
    }
}
