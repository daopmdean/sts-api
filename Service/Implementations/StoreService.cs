using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;
using Data.Repositories.Interfaces;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _repository;
        private readonly IMapper _mapper;

        public StoreService(IStoreRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Store> CreateStore(StoreCreate storeCreate)
        {
            var store = _mapper.Map<Store>(storeCreate);
            await _repository.CreateAsync(store);

            if (await _repository.SaveChangesAsync())
                return store;

            throw new AppException(400, "Can not create store");
        }

        public async Task DeleteStore(int id)
        {
            var store = await _repository.GetByIdAsync(id);

            if (store == null)
                throw new AppException(400, "Store not found");

            _repository.Delete(store);

            if (await _repository.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not delete store");
        }

        public async Task<Store> GetStore(int id)
        {
            var store = await _repository.GetByIdAsync(id);

            if (store == null)
                throw new AppException(400, "Store not found or has been deleted");

            return store;
        }

        public async Task<PagedList<StoreOverview>> GetStores(StoreParams @params)
        {
            return await _repository.GetStoresAsync(@params);
        }

        public async Task UpdateStore(int id, StoreUpdate storeUpdate)
        {
            var store = await _repository.GetByIdAsync(id);

            if (store == null)
                throw new AppException(400, "Store not found");

            _mapper.Map(storeUpdate, store);

            _repository.Update(store);

            if (await _repository.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not update brand");
        }
    }
}
