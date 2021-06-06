﻿using System.Threading.Tasks;
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
        Task<UserInfoResponse> GetUserAsync(string username);
        Task CreateStoreManager(RegisterRequest user);
        Task CreateStaff(RegisterRequest user);
        Task UpdateUserAsync(string username, UserUpdate updateInfo);
        Task DeleteUserAsync(string username);
    }
}
