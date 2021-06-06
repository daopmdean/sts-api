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
        Task<Skill> GetSkill(int id);
        Task<Skill> CreateSkill(SkillCreate skillCreate);
        Task UpdateSkill(int id, SkillUpdate skillUpdate);
        Task DeleteSkill(int id);
    }
}
