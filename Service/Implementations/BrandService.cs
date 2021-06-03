using System;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;
using Data.Repositories.Interfaces;
using Service.Interfaces;

namespace Service.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _repository;

        public BrandService(IBrandRepository repository)
        {
            _repository = repository;
        }

        public Task<Brand> GetBrand(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<BrandOverview>> GetBrands(BrandParams @params)
        {
            return await _repository.GetBrandsAsync(@params);
        }
    }
}
