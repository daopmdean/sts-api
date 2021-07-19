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
        Task<PagedList<UserOverview>> GetBrandManagers(UserParams @params);
        Task<PagedList<UserOverview>> GetStoreManagers(UserParams @params);
        Task<PagedList<UserOverview>> GetStaff(UserParams @params);
        Task<UserInfoResponse> GetUserAsync(string username);
        Task<UserGeneralResponse> GetUserGeneralAsync(string username);
        Task UpdateUserAsync(string username, UserUpdate updateInfo);
        Task UpdatePasswordAsync(string username, PasswordUpdate update);
        Task DeleteUserAsync(string username);
    }
}
