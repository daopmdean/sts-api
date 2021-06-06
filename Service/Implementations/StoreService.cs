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
        private readonly IStoreRepository _storeRepo;
        private readonly IBrandRepository _brandRepo;
        private readonly IMapper _mapper;

        public StoreService(IStoreRepository storeRepo,
            IBrandRepository brandRepo, IMapper mapper)
        {
            _storeRepo = storeRepo;
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        public async Task<Store> CreateStore(StoreCreate storeCreate)
        {
            var brand = await _brandRepo.GetByIdAsync(storeCreate.BrandId);

            if (brand == null)
                throw new AppException(400,
                    "Conflicted with the FOREIGN KEY constraint, brandId does not exist");

            var store = _mapper.Map<Store>(storeCreate);
            await _storeRepo.CreateAsync(store);

            if (await _storeRepo.SaveChangesAsync())
                return store;

            throw new AppException(400, "Can not create store");
        }

        public async Task DeleteStore(int id)
        {
            var store = await _storeRepo.GetByIdAsync(id);

            if (store == null)
                throw new AppException(400, "Store not found");

            _storeRepo.Delete(store);

            if (await _storeRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not delete store");
        }

        public async Task<Store> GetStore(int id)
        {
            var store = await _storeRepo.GetByIdAsync(id);

            if (store == null)
                throw new AppException(400, "Store not found or has been deleted");

            return store;
        }

        public async Task<PagedList<StoreOverview>> GetStores(StoreParams @params)
        {
            return await _storeRepo.GetStoresAsync(@params);
        }

        public async Task<PagedList<StoreOverview>> GetStores(int brandId,
            StoreParams @params)
        {
            return await _storeRepo.GetStoresAsync(brandId, @params);
        }

        public async Task UpdateStore(int id, StoreUpdate storeUpdate)
        {
            var store = await _storeRepo.GetByIdAsync(id);

            if (store == null)
                throw new AppException(400, "Store not found");

            _mapper.Map(storeUpdate, store);

            _storeRepo.Update(store);

            if (await _storeRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not update brand");
        }
    }
}
