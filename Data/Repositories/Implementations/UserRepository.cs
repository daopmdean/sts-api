using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserOverview>> GetAllUsersAsync()
        {
            return await _entities
                .ProjectTo<UserOverview>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<PagedList<UserOverview>> GetBrandManagersAsync(
            UserParams @params)
        {
            var query = _entities
                .AsQueryable()
                .OrderBy(u => u.Username)
                .Where(u => u.RoleId == 2)
                .ProjectTo<UserOverview>(_mapper.ConfigurationProvider);

            return await PagedList<UserOverview>
                .CreateAsync(query, @params.PageNumber, @params.PageSize);
        }

        public async Task<PagedList<UserOverview>> GetStaffAsync(UserParams @params)
        {
            var query = _entities
                .AsQueryable()
                .OrderBy(u => u.Username)
                .Where(u => u.RoleId == 4)
                .ProjectTo<UserOverview>(_mapper.ConfigurationProvider);

            return await PagedList<UserOverview>
                .CreateAsync(query, @params.PageNumber, @params.PageSize);
        }

        public async Task<PagedList<UserOverview>> GetStoreManagersAsync(
            UserParams @params)
        {
            var query = _entities
                .AsQueryable()
                .OrderBy(u => u.Username)
                .Where(u => u.RoleId == 3)
                .ProjectTo<UserOverview>(_mapper.ConfigurationProvider);

            return await PagedList<UserOverview>
                .CreateAsync(query, @params.PageNumber, @params.PageSize);
        }
    }
}
