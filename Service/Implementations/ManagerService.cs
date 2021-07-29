﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Data.Enums;
using Data.Models.Requests;
using Data.Repositories.Interfaces;
using Service.Enums;
using Service.Exceptions;
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
        private readonly IUserRepository _userRepo;
        private readonly IWeekScheduleRepository _weekScheduleRepo;
        private readonly IShiftAssignmentRepository _shiftAssignmentRepo;
        private readonly IStoreRepository _storeRepo;
        private readonly ISkillRepository _skillRepo;
        private readonly IMapper _mapper;

        public ManagerService(
            IAuthService authService,
            IStoreStaffService storeStaffService,
            IStaffSkillService staffSkillService,
            IBrandService brandService,
            IEmailSender emailSender,
            IUserRepository userRepo,
            IWeekScheduleRepository weekScheduleRepo,
            IShiftAssignmentRepository shiftAssignmentRepo,
            IStoreRepository storeRepo,
            ISkillRepository skillRepo,
            IMapper mapper)
        {
            _authService = authService;
            _storeStaffService = storeStaffService;
            _staffSkillService = staffSkillService;
            _brandService = brandService;
            _emailSender = emailSender;
            _userRepo = userRepo;
            _weekScheduleRepo = weekScheduleRepo;
            _shiftAssignmentRepo = shiftAssignmentRepo;
            _storeRepo = storeRepo;
            _skillRepo = skillRepo;
            _mapper = mapper;
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

        public async Task<ShiftAssignment> PublishSchedule(
            PublishInfo create)
        {
            var weekSchedule = await _weekScheduleRepo
                .GetByIdAsync(create.WeekScheduleId);

            if (weekSchedule == null)
                throw new AppException((int)StatusCode.BadRequest,
                    "WeekSchedule not found");

            if (weekSchedule.Status != Status.Unpublished)
                throw new AppException((int)StatusCode.BadRequest,
                    "Can only publish unpublished week schedule");

            var publishedWeekSchedule = await _weekScheduleRepo
                .GetWeekSchedulesAsync(weekSchedule.StoreId, weekSchedule.DateStart, Status.Published);

            if (publishedWeekSchedule == null)
            {
                weekSchedule.Status = Status.Published;
                _weekScheduleRepo.Update(weekSchedule);
            }
            else
            {
                foreach (var schedule in publishedWeekSchedule)
                {
                    schedule.Status = Status.Unpublished;
                    _weekScheduleRepo.Update(schedule);

                    var shiftAssignments = await _shiftAssignmentRepo
                        .GetShiftAssignmentsAsync(create.WeekScheduleId, DateTime.Now);

                    foreach (var shiftAssignment in shiftAssignments)
                    {
                        _shiftAssignmentRepo.Delete(shiftAssignment);
                    }
                }
            }

            foreach (var shift in create.ShiftAssignments)
            {
                var store = await _storeRepo
                    .GetByIdAsync(shift.StoreId);
                if (store == null)
                    throw new AppException(400,
                        "Conflicted with the FOREIGN KEY constraint, StoreId does not exist");

                var skill = await _skillRepo
                    .GetByIdAsync(shift.SkillId);
                if (skill == null)
                    throw new AppException(400,
                        "Conflicted with the FOREIGN KEY constraint, SkillId does not exist");

                var user = await _userRepo
                    .GetUserByUsernameAsync(shift.Username);
                if (user == null)
                    throw new AppException(400,
                        "Conflicted with the FOREIGN KEY constraint, Username does not exist");

                var shiftAssignment = _mapper.Map<ShiftAssignment>(shift);
                shiftAssignment.WeekScheduleId = create.WeekScheduleId;
                await _shiftAssignmentRepo.CreateAsync(shiftAssignment);
            }

            if (await _shiftAssignmentRepo.SaveChangesAsync())
                return null;

            throw new AppException(400, "Can not create ShiftAssignments");
        }

        public async Task UnpublishSchedule(
            UnpublishInfo create)
        {
            var weekSchedule = await _weekScheduleRepo
                .GetByIdAsync(create.WeekScheduleId);

            if (weekSchedule == null)
                throw new AppException((int)StatusCode.BadRequest,
                    "WeekSchedule not found");

            if (weekSchedule.Status != Status.Published)
                throw new AppException((int)StatusCode.BadRequest,
                    "Can only unpublish published week schedule");

            weekSchedule.Status = Status.Unpublished;
            _weekScheduleRepo.Update(weekSchedule);

            var shiftAssignments = await _shiftAssignmentRepo
                        .GetShiftAssignmentsAsync(create.WeekScheduleId, DateTime.Now);

            foreach (var shiftAssignment in shiftAssignments)
            {
                _shiftAssignmentRepo.Delete(shiftAssignment);
            }

            if (await _weekScheduleRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not create ShiftAssignments");
        }
    }
}
