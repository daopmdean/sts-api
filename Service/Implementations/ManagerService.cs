using System;
using System.Threading.Tasks;
using Data.Models.Requests;
using Data.Repositories.Interfaces;
using Service.Enums;
using Service.Helpers;
using Service.Interfaces;

namespace Service.Implementations
{
    public class ManagerService : IManagerService
    {
        private readonly IAuthService _authService;
        private readonly IStoreStaffService _storeStaffService;
        private readonly IStaffSkillService _staffSkillService;
        private readonly IEmailSender _emailSender;

        public ManagerService(
            IAuthService authService,
            IStoreStaffService storeStaffService,
            IStaffSkillService staffSkillService,
            IEmailSender emailSender)
        {
            _authService = authService;
            _storeStaffService = storeStaffService;
            _staffSkillService = staffSkillService;
            _emailSender = emailSender;
        }

        public Task AssignStoreManager(StoreAssign brandAssign)
        {
            throw new NotImplementedException();
        }

        public async Task<StaffCreate> CreateStaff(
            int brandId, StaffCreate info)
        {
            var staffInfo = info.GeneralInfo;
            staffInfo.Password = Helper.GenerateRandomPassword(6);
            await _authService
                .RegisterWithRole(brandId, (int)UserRole.Staff, staffInfo);

            var skills = info.StaffSkills;
            foreach (var skill in skills)
            {
                skill.Username = staffInfo.Username;
                await _staffSkillService.CreateStaffSkill(skill);
            }

            var storeStaff = info.JobInformation;
            storeStaff.Username = staffInfo.Username;
            await _storeStaffService.CreateStoreStaff(storeStaff);

            await _emailSender.SendEmailAsync(new Message(
                new string[] { staffInfo.Email },
                "STS staff account",
                "<p>You are invited with username: " + staffInfo.Username + "</p>" +
                "password: " + staffInfo.Password));

            return info;
        }
    }
}
