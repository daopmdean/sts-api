using System.Threading.Tasks;
using Data.Models.Responses;
using Data.Pagings;

namespace Service.Interfaces
{
    public interface IReportService
    {
        Task<WorkHoursResponse> GetWorkHoursResponse(
            string username, WorkHoursReportParams @params);
    }
}
