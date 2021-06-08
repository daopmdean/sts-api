using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;
using Data.Repositories.Interfaces;

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

        public Task<PagedList<StoreStaffOverview>> GetStaffFromStoreAsync(
            int storeId, StoreStaffParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<StoreStaffOverview>> GetStoresFromStaffAsync(
            string username, StoreStaffParams @params)
        {
            throw new NotImplementedException();
        }
    }
}
