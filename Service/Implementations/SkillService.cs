using System;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;
using Service.Interfaces;

namespace Service.Implementations
{
    public class SkillService : ISkillService
    {
        public Task<Store> CreateStore(StoreCreate storeCreate)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStore(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<SkillOverview>> GetSkills(SkillParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<SkillOverview>> GetSkills(int brandId, SkillParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<Store> GetStore(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStore(int id, StoreUpdate storeUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
