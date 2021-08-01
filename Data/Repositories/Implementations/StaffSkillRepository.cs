using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class StaffSkillRepository :
        BaseRepository<StaffSkill>,
        IStaffSkillRepository
    {
        private readonly IMapper _mapper;

        public StaffSkillRepository(DataContext context,
            IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedList<StaffSkillOverview>> GetSkillsFromStaffAsync(
            string username, StaffSkillParams @params)
        {
            var source = _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.Username == username)
                .OrderBy(s => s.SkillId)
                .ProjectTo<StaffSkillOverview>(_mapper.ConfigurationProvider);

            return await PagedList<StaffSkillOverview>
                .CreateAsync(source, @params.PageNumber, @params.PageSize);
        }

        public async Task<IEnumerable<StaffSkillOverview>> GetSkillsFromStaffAsync(
            string username)
        {
            return await _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.Username == username)
                .OrderBy(s => s.SkillId)
                .ProjectTo<StaffSkillOverview>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<PagedList<StaffSkillOverview>> GetStaffFromSkillAsync(
            int skillId, StaffSkillParams @params)
        {
            var source = _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.SkillId == skillId)
                .OrderBy(s => s.Username)
                .ProjectTo<StaffSkillOverview>(_mapper.ConfigurationProvider);

            return await PagedList<StaffSkillOverview>
                .CreateAsync(source, @params.PageNumber, @params.PageSize);
        }

        public async Task<StaffSkill> GetStaffSkillAsync(
            int skillId, string username)
        {
            return await _entities.FirstOrDefaultAsync(ss
                => ss.SkillId == skillId
                && ss.Username == username
                && ss.Status == Enums.Status.Active);
        }

        public override void Delete(StaffSkill entity)
        {
            _entities.Remove(entity);
        }
    }
}
