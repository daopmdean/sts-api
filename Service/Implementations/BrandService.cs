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
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _repository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Brand> CreateBrand(BrandCreate brandCreate)
        {
            var brand = _mapper.Map<Brand>(brandCreate);
            await _repository.CreateAsync(brand);

            if (await _repository.SaveChangesAsync())
                return brand;

            throw new AppException(400, "Can not create brand");
        }

        public async Task DeleteBrand(int id)
        {
            var brand = await _repository.GetByIdAsync(id);
            _repository.Delete(brand);

            if (await _repository.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not delete brand");
        }

        public async Task<Brand> GetBrand(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<PagedList<BrandOverview>> GetBrands(BrandParams @params)
        {
            return await _repository.GetBrandsAsync(@params);
        }

        public async Task UpdateBrand(int id, BrandUpdate brandUpdate)
        {
            var brand = await _repository.GetByIdAsync(id);
            _mapper.Map(brandUpdate, brand);

            if (await _repository.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not update brand");
        }
    }
}
