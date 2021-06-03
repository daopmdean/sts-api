using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;

namespace Data.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<PagedList<UserOverview>> GetBrandManagersAsync(UserParams @params);
        Task<PagedList<UserOverview>> GetStoreManagersAsync(UserParams @params);
        Task<PagedList<UserOverview>> GetStaffAsync(UserParams @params);
        Task<User> GetByUsernameAsync(string username);
    }
}
