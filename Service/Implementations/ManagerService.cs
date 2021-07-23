﻿using System.Threading.Tasks;
using Data.Models.Requests;
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
        private readonly IBrandService _brandService;
        private readonly IEmailSender _emailSender;

        public ManagerService(
            IAuthService authService,
            IStoreStaffService storeStaffService,
            IStaffSkillService staffSkillService,
            IBrandService brandService,
            IEmailSender emailSender)
        {
            _authService = authService;
            _storeStaffService = storeStaffService;
            _staffSkillService = staffSkillService;
            _brandService = brandService;
            _emailSender = emailSender;
        }

        public async Task<BrandManagerCreate> CreateBrandManager(
            BrandManagerCreate info)
        {
            var brandCreate = info.Brand;
            var brand = await _brandService.CreateBrand(brandCreate);

            var brandManagerInfo = info.GeneralInfo;
            await _authService
                .RegisterWithRole(brand.Id,
                    (int)UserRole.BrandManager, brandManagerInfo);

            await _emailSender.SendEmailAsync(new Message(
                new string[] { brandManagerInfo.Email },
                "STS welcome you on board",
                "<p>You have successfully register with username: " + brandManagerInfo.Username + "</p>" +
                "<p>We hope you will have the best experience with us</p>"));

            return info;
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
                "<p>You are invited as staff.</p>" +
                "<p>Your username: " + staffInfo.Username + "</p>" +
                "<p>password: " + staffInfo.Password + "</p>"));

            return info;
        }

        public async Task<StoreManagerCreate> CreateStoreManager(
            int brandId, StoreManagerCreate info)
        {
            var storeManagerInfo = info.GeneralInfo;
            storeManagerInfo.Password = Helper.GenerateRandomPassword(6);
            await _authService
                .RegisterWithRole(brandId,
                    (int)UserRole.StoreManager, storeManagerInfo);

            var storeStaff = info.JobInformation;
            storeStaff.Username = storeManagerInfo.Username;
            await _storeStaffService.CreateStoreStaff(storeStaff);

            await _emailSender.SendEmailAsync(new Message(
                new string[] { storeManagerInfo.Email },
                "STS store manager account",
                "<p>You are invited as store manager</p>" +
                "<p>Your username: " + storeManagerInfo.Username + "</p>" +
                "<p>password: " + storeManagerInfo.Password + "</p>"));

            return info;
        }
    }
}
