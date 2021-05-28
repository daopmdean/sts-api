using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Models.Responses;
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

        public async Task<IEnumerable<UserOverview>> GetUserOverviews()
        {
            //var users = (await _repository.GetAllAsync()).AsQueryable();
            //users = users.Where(user => user.Username.Contains(""));

            //var result = users.ProjectTo<UserOverview>(_mapper.ConfigurationProvider);

            return await _repository.GetAllUsersAsync();
        }
    }
}
