using System.Threading.Tasks;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;

namespace Service.Interfaces
{
    public interface IUserService
    {
        Task<PagedList<UserOverview>> GetUsersAsync(UserParams @params);
        Task<PagedList<UserOverview>> GetUsersAsync(int brandId,
            UserParams @params);
        Task<PagedList<UserOverview>> GetStaffAsync(int brandId,
            UserParams @params);
        Task<PagedList<UserOverview>> GetBrandManagersAsync(UserParams @params);
        Task<PagedList<UserOverview>> GetStoreManagersAsync(
            int brandId, UserParams @params);
        Task<PagedList<UserOverview>> GetStaff(UserParams @params);
        Task<UserInfoResponse> GetUserAsync(string username);
        Task<UserGeneralResponse> GetUserGeneralAsync(string username);
        Task<WorkHoursResponse> GetWorkHoursResponse(
            string username, DateTimeParams @params);
        Task UpdateUserAsync(string username, UserUpdate updateInfo);
        Task UpdateUserAsync(StaffUpdateRequest updateInfo);
        Task UpdatePasswordAsync(string username, PasswordUpdate update);
        Task RestorePasswordAsync(string username);
        Task DeleteUserAsync(string username);
    }
}
