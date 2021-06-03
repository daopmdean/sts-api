using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Models.Responses;
using Data.Pagings;
using Data.Repositories.Interfaces;
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
    }
}
