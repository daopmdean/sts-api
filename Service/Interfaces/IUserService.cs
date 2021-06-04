using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;

namespace Service.Interfaces
{
    public interface IUserService
    {
        Task<PagedList<UserOverview>> GetBrandManagers(UserParams @params);
        Task<PagedList<UserOverview>> GetStoreManagers(UserParams @params);
        Task<PagedList<UserOverview>> GetStaff(UserParams @params);
        Task<UserInfoResponse> GetUser(string username);
        Task UpdateUser(string username, UserUpdate updateInfo);
    }
}
