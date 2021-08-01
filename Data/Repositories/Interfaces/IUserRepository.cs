using System.Threading.Tasks;
using Data.Entities;
using Data.Enums;
using Data.Models.Responses;
using Data.Pagings;

namespace Data.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<PagedList<UserOverview>> GetUsersAsync(UserParams @params);
        Task<PagedList<UserOverview>> GetUsersAsync(int brandId,
            UserParams @params);
        Task<PagedList<UserOverview>> GetStaffAsync(int brandId,
            UserParams @params);
        Task<PagedList<UserOverview>> GetBrandManagersAsync(UserParams @params);
        Task<PagedList<UserOverview>> GetStoreManagersAsync(
            int brandId, UserParams @params);
        Task<PagedList<UserOverview>> GetStaffAsync(UserParams @params);
        Task<User> GetUserByUsernameAsync(string username);
        Task<StaffType> GetStaffTypeAsync(string username);
        Task<UserInfoResponse> GetByUsernameAsync(string username);
    }
}
