using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;

namespace Service.Interfaces
{
    public interface IBrandService
    {
        Task<PagedList<BrandOverview>> GetBrands(BrandParams @params);
        Task<Brand> GetBrand(int id);
    }
}
