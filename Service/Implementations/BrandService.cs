using System;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;
using Service.Interfaces;

namespace Service.Implementations
{
    public class BrandService : IBrandService
    {
        public BrandService()
        {
        }

        public Task<Brand> GetBrand(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<BrandOverview>> GetBrands(BrandParams @params)
        {
            throw new NotImplementedException();
        }
    }
}
