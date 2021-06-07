using System.Threading.Tasks;
using Data.Models.Responses;
using Data.Pagings;

namespace Service.Interfaces
{
    public interface IStaffScheduleDetailService
    {
        Task<PagedList<StaffScheduleDetailOverview>> GetStaffScheduleDetails(
            int weekScheduleId, StaffScheduleDetailParams @params);
    }
}
