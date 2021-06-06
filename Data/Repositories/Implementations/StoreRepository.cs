﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;
using Data.Repositories.Interfaces;

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

        public async Task<PagedList<StoreOverview>> GetStoresAsync(StoreParams @params)
        {
            var source = _entities
                .Where(b => b.Status == Enums.Status.Active)
                .OrderBy(b => b.Name)
                .ProjectTo<StoreOverview>(_mapper.ConfigurationProvider);

            return await PagedList<StoreOverview>
                .CreateAsync(source, @params.PageNumber, @params.PageSize);
        }
    }
}
