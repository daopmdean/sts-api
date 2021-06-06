using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;

namespace Service.Interfaces
{
    public interface ISkillService
    {
        Task<PagedList<SkillOverview>> GetSkills(SkillParams @params);
        Task<PagedList<SkillOverview>> GetSkills(int brandId,
            SkillParams @params);
        Task<Store> GetStore(int id);
        Task<Store> CreateStore(StoreCreate storeCreate);
        Task UpdateStore(int id, StoreUpdate storeUpdate);
        Task DeleteStore(int id);
    }
}
