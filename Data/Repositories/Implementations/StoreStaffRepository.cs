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
    public class StoreStaffRepository : BaseRepository<StoreStaff>,
        IStoreStaffRepository
    {
        private readonly IMapper _mapper;

        public StoreStaffRepository(DataContext context,
            IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedList<StoreStaffOverview>> GetStaffFromStoreAsync(
            int storeId, StoreStaffParams @params)
        {
            var source = _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.StoreId == storeId)
                .Where(s => s.Username.Contains(@params.Keyword)
                    || s.User.Email.Contains(@params.Keyword)
                    || s.User.FirstName.Contains(@params.Keyword)
                    || s.User.LastName.Contains(@params.Keyword)
                    || s.User.Address.Contains(@params.Keyword))
                .OrderBy(s => s.Username)
                .ProjectTo<StoreStaffOverview>(_mapper.ConfigurationProvider);

            return await PagedList<StoreStaffOverview>
                .CreateAsync(source, @params.PageNumber, @params.PageSize);
        }

        public async Task<int> GetStoreId(string username)
        {
            var storeStaff = await _entities
                .FirstOrDefaultAsync(ss => ss.Username == username
                    && ss.Status == Enums.Status.Active);

            if (storeStaff == null)
                return -1;

            return storeStaff.StoreId;
        }

        public async Task<int> GetStaffStoreId(string username)
        {
            var storeStaff = await _entities
                .FirstOrDefaultAsync(ss => ss.Username == username
                    && ss.Status == Enums.Status.Active
                    && ss.IsPrimaryStore == true);

            if (storeStaff == null)
                return -1;

            return storeStaff.StoreId;
        }

        public async Task<PagedList<StoreStaffOverview>> GetStoresFromStaffAsync(
            string username, StoreStaffParams @params)
        {
            var source = _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.Username == username)
                .OrderBy(s => s.StoreId)
                .ProjectTo<StoreStaffOverview>(_mapper.ConfigurationProvider);

            return await PagedList<StoreStaffOverview>
                .CreateAsync(source, @params.PageNumber, @params.PageSize);
        }

        public async Task<StoreStaff> GetStoreStaffAsync(int storeId,
            string username)
        {
            return await _entities.FirstOrDefaultAsync(ss
                => ss.StoreId == storeId
                && ss.Username == username
                && ss.Status == Enums.Status.Active);
        }

        public async Task<IEnumerable<StoreStaffOverview>> GetStaffFromStoreAsync(
            int storeId)
        {
            return await _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.StoreId == storeId)
                .OrderBy(s => s.Username)
                .ProjectTo<StoreStaffOverview>(_mapper.ConfigurationProvider)
                .ToListAsync();

        }
    }
}
