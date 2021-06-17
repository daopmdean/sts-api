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

        public UserRepository(DataContext context,
            IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserOverview>> GetAllUsersAsync()
        {
            return await _entities
                .Where(e => e.Status == Enums.Status.Active)
                .ProjectTo<UserOverview>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<PagedList<UserOverview>> GetUsersAsync(
            UserParams @params)
        {
            var query = _entities
                .OrderBy(u => u.Username)
                .Where(e => e.Status == Enums.Status.Active)
                .Where(u => u.Username.Contains(@params.Keyword)
                    || u.FirstName.Contains(@params.Keyword)
                    || u.LastName.Contains(@params.Keyword)
                    || u.Address.Contains(@params.Keyword)
                    || u.Email.Contains(@params.Keyword))
                .ProjectTo<UserOverview>(_mapper.ConfigurationProvider);

            return await PagedList<UserOverview>
                .CreateAsync(query, @params.PageNumber, @params.PageSize);
        }

        public async Task<PagedList<UserOverview>> GetUsersAsync(int brandId,
            UserParams @params)
        {
            var query = _entities
                .OrderBy(u => u.Username)
                .Where(u => u.Status == Enums.Status.Active)
                .Where(u => u.BrandId == brandId)
                .ProjectTo<UserOverview>(_mapper.ConfigurationProvider);

            return await PagedList<UserOverview>
                .CreateAsync(query, @params.PageNumber, @params.PageSize);
        }

        public async Task<PagedList<UserOverview>> GetBrandManagersAsync(
            UserParams @params)
        {
            var query = _entities
                .AsQueryable()
                .OrderBy(u => u.Username)
                .Where(u => u.RoleId == 2)
                .Where(e => e.Status == Enums.Status.Active)
                .ProjectTo<UserOverview>(_mapper.ConfigurationProvider);

            return await PagedList<UserOverview>
                .CreateAsync(query, @params.PageNumber, @params.PageSize);
        }

        public async Task<UserInfoResponse> GetByUsernameAsync(string username)
        {
            var user = await _entities
                .Where(e => e.Status == Enums.Status.Active)
                .FirstOrDefaultAsync(user => user.Username == username);

            return _mapper.Map<UserInfoResponse>(user);
        }

        public async Task<PagedList<UserOverview>> GetStaffAsync(
            UserParams @params)
        {
            // not implemented
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
            // not implemented
            //var brand = await _context.Brands
            //    .FirstOrDefaultAsync(b => b.Id == @params.BrandId);

            var query = _entities
                .AsQueryable()
                .OrderBy(u => u.Username)
                .Where(u => u.RoleId == 3)
                .ProjectTo<UserOverview>(_mapper.ConfigurationProvider);

            return await PagedList<UserOverview>
                .CreateAsync(query, @params.PageNumber, @params.PageSize);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _entities
                .Where(e => e.Status == Enums.Status.Active)
                .FirstOrDefaultAsync(user => user.Username == username);
        }


    }
}
