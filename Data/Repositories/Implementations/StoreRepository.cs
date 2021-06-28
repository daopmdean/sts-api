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
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        private readonly IMapper _mapper;

        public StoreRepository(DataContext context
            , IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedList<StoreOverview>> GetStoresAsync(
            StoreParams @params)
        {
            var source = _entities
                .Where(b => b.Status == Enums.Status.Active)
                .OrderBy(b => b.Name)
                .ProjectTo<StoreOverview>(_mapper.ConfigurationProvider);

            return await PagedList<StoreOverview>
                .CreateAsync(source, @params.PageNumber, @params.PageSize);
        }

        public async Task<PagedList<StoreOverview>> GetStoresAsync(int brandId,
            StoreParams @params)
        {
            var source = _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.BrandId == brandId)
                .OrderBy(s => s.Name)
                .ProjectTo<StoreOverview>(_mapper.ConfigurationProvider);

            return await PagedList<StoreOverview>
                .CreateAsync(source, @params.PageNumber, @params.PageSize);
        }

        public async Task<IEnumerable<StoreOverview>> GetStoresAsync(int brandId)
        {
            return await _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.BrandId == brandId)
                .OrderBy(s => s.Name)
                .ProjectTo<StoreOverview>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
