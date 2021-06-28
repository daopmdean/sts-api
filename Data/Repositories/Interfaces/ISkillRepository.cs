using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;

namespace Data.Repositories.Interfaces
{
    public interface ISkillRepository : IBaseRepository<Skill>
    {
        Task<PagedList<SkillOverview>> GetSkillsAsync(int brandId,
            SkillParams @params);
        Task<IEnumerable<SkillOverview>> GetSkillsAsync(int brandId);
    }
}
