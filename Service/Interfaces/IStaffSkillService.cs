using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;

namespace Service.Interfaces
{
    public interface IStaffSkillService
    {
        Task<PagedList<StaffSkillOverview>> GetSkillsFromStaffAsync(
            string username, StaffSkillParams @params);
        Task<IEnumerable<StaffSkillOverview>> GetSkillsFromStaffAsync(
            string username);
        Task<PagedList<StaffSkillOverview>> GetStaffFromSkillAsync(
            int skillId, StaffSkillParams @params);
        Task<StaffSkill> GetStaffSkillAsync(int skillId, string username);
        Task<StaffSkill> CreateStaffSkill(StaffSkillCreate create);
        Task UpdateStaffSkill(int skillId, string username,
            StaffSkillUpdate update);
        Task DeleteStaffSkill(int skillId, string username);
    }
}
