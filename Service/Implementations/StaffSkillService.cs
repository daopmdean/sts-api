using System;
using System.Collections.Generic;
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
    public class StaffSkillService : IStaffSkillService
    {
        private readonly IStaffSkillRepository _staffSkillRepo;
        private readonly ISkillRepository _skillRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public StaffSkillService(
            IStaffSkillRepository staffSkillRepo,
            ISkillRepository skillRepo,
            IUserRepository userRepo,
            IMapper mapper)
        {
            _staffSkillRepo = staffSkillRepo;
            _skillRepo = skillRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<StaffSkill> CreateStaffSkill(StaffSkillCreate create)
        {
            var skill = await _skillRepo.GetByIdAsync(create.SkillId);

            if (skill == null)
                throw new AppException(400,
                    "Conflicted with the FOREIGN KEY constraint, SkillId does not exist");

            var user = await _userRepo.GetUserByUsernameAsync(create.Username);

            if (user == null)
                throw new AppException(400,
                    "Conflicted with the FOREIGN KEY constraint, Username does not exist");

            var staffSkill = _mapper.Map<StaffSkill>(create);
            await _staffSkillRepo.CreateAsync(staffSkill);

            if (await _staffSkillRepo.SaveChangesAsync())
                return staffSkill;

            throw new AppException(400, "Can not create StaffSkill");
        }

        public async Task DeleteStaffSkill(int skillId, string username)
        {
            var staffSkill = await _staffSkillRepo
                .GetStaffSkillAsync(skillId, username);

            if (staffSkill == null)
                throw new AppException(400, "StaffSkill not found");

            _staffSkillRepo.Delete(staffSkill);

            if (await _staffSkillRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not delete StaffSkill");
        }

        public async Task<PagedList<StaffSkillOverview>> GetSkillsFromStaffAsync(
            string username, StaffSkillParams @params)
        {
            return await _staffSkillRepo
                .GetSkillsFromStaffAsync(username, @params);
        }

        public async Task<IEnumerable<StaffSkillOverview>> GetSkillsFromStaffAsync(
            string username)
        {
            return await _staffSkillRepo
                .GetSkillsFromStaffAsync(username);
        }

        public async Task<PagedList<StaffSkillOverview>> GetStaffFromSkillAsync(
            int skillId, StaffSkillParams @params)
        {
            return await _staffSkillRepo
                .GetStaffFromSkillAsync(skillId, @params);
        }

        public async Task<StaffSkill> GetStaffSkillAsync(
            int skillId, string username)
        {
            var staffSkill = await _staffSkillRepo
                .GetStaffSkillAsync(skillId, username);

            if (staffSkill == null)
                throw new AppException(400,
                    "StaffSkill not found or has been deleted");

            return staffSkill;
        }

        public async Task UpdateStaffSkill(
            int skillId, string username, StaffSkillUpdate update)
        {
            var staffSkill = await _staffSkillRepo
                .GetStaffSkillAsync(skillId, username);

            if (staffSkill == null)
                throw new AppException(400, "StaffSkill not found");

            _mapper.Map(update, staffSkill);

            _staffSkillRepo.Update(staffSkill);

            if (await _staffSkillRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not update StaffSkill");
        }
    }
}
