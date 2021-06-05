using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;
using Data.Repositories.Interfaces;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task CreateStaff(RegisterRequest user)
        {
            throw new NotImplementedException();
        }

        public Task CreateStoreManager(RegisterRequest user)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteUserAsync(string username)
        {
            var user = await _repository.GetUserByUsernameAsync(username);
            _repository.Delete(user);

            if (await _repository.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not delete user");
        }

        public async Task<PagedList<UserOverview>> GetBrandManagers(UserParams @params)
        {
            return await _repository.GetBrandManagersAsync(@params);
        }

        public async Task<PagedList<UserOverview>> GetStaff(UserParams @params)
        {
            return await _repository.GetStaffAsync(@params);
        }

        public async Task<PagedList<UserOverview>> GetStoreManagers(UserParams @params)
        {
            return await _repository.GetStoreManagersAsync(@params);
        }

        public async Task<UserInfoResponse> GetUserAsync(string username)
        {
            return await _repository.GetByUsernameAsync(username);
        }

        public async Task UpdateUserAsync(string username,
            UserUpdate updateInfo)
        {
            var user = await _repository.GetUserByUsernameAsync(username);

            _mapper.Map(updateInfo, user);
            _repository.Update(user);

            if (await _repository.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not update user");
        }
    }
}
