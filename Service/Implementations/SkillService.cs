using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;
using Data.Repositories.Interfaces;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepo;
        private readonly IBrandRepository _brandRepo;
        private readonly IMapper _mapper;

        public SkillService(ISkillRepository skillRepo,
            IBrandRepository brandRepo, IMapper mapper)
        {
            _skillRepo = skillRepo;
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        public async Task<Skill> CreateSkill(
            int brandId, SkillCreate skillCreate)
        {
            var skill = _mapper.Map<Skill>(skillCreate);
            skill.BrandId = brandId;
            await _skillRepo.CreateAsync(skill);

            if (await _skillRepo.SaveChangesAsync())
                return skill;

            throw new AppException(400, "Can not create skill");
        }

        public async Task DeleteSkill(int id)
        {
            var skill = await _skillRepo.GetByIdAsync(id);

            if (skill == null)
                throw new AppException(400, "Skill not found");

            _skillRepo.Delete(skill);

            if (await _skillRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not delete skill");
        }

        public async Task<Skill> GetSkill(int id)
        {
            var skill = await _skillRepo.GetByIdAsync(id);

            if (skill == null)
                throw new AppException(400, "Skill not found or has been deleted");

            return skill;
        }

        public Task<PagedList<SkillOverview>> GetSkills(SkillParams @params)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<SkillOverview>> GetSkills(int brandId,
            SkillParams @params)
        {
            return await _skillRepo.GetSkillsAsync(brandId, @params);
        }

        public async Task UpdateSkill(int id, SkillUpdate skillUpdate)
        {
            var skill = await _skillRepo.GetByIdAsync(id);

            if (skill == null)
                throw new AppException(400, "Skill not found");

            _mapper.Map(skillUpdate, skill);

            _skillRepo.Update(skill);

            if (await _skillRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not update skill");
        }
    }
}
