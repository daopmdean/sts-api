using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Entities;
using Data.Repositories.Interfaces;
using Service.Interfaces;
using Service.Models.Responses;

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
            IQueryable<User> users = (await _repository.GetAllAsync()).AsQueryable();
            users = users.Where(user => user.Username.Contains(""));

            var result = users.ProjectTo<UserOverview>(_mapper.ConfigurationProvider);
            return result;
        }
    }
}
