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
                .OrderBy(s => s.Username)
                .ProjectTo<StoreStaffOverview>(_mapper.ConfigurationProvider);

            return await PagedList<StoreStaffOverview>
                .CreateAsync(source, @params.PageNumber, @params.PageSize);
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
    }
}
