using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;

namespace Service.Interfaces
{
    public interface IBrandService
    {
        Task<PagedList<BrandOverview>> GetBrands(BrandParams @params);
        Task<Brand> GetBrand(int id);
        Task<Brand> CreateBrand(BrandCreate brandCreate);
        Task UpdateBrand(int id, BrandUpdate brandUpdate);
        Task DeleteBrand(int id);
    }
}
