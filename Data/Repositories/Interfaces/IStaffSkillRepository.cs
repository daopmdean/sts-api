using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;

namespace Data.Repositories.Interfaces
{
    public interface IStaffSkillRepository : IBaseRepository<StaffSkill>
    {
        Task<PagedList<StaffSkillOverview>> GetStaffFromSkillAsync(
            int skillId, StaffSkillParams @params);
        Task<PagedList<StaffSkillOverview>> GetSkillsFromStaffAsync(
            string username, StaffSkillParams @params);
        Task<IEnumerable<StaffSkillOverview>> GetSkillsFromStaffAsync(
            string username);
        Task<StaffSkill> GetStaffSkillAsync(int skillId, string username);
    }
}
