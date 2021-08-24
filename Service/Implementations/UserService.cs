using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;
using Data.Repositories.Interfaces;
using Service.Enums;
using Service.Exceptions;
using Service.Helpers;
using Service.Interfaces;

namespace Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IShiftAssignmentRepository _shiftAssignmentRepo;
        private readonly IStoreStaffRepository _storeStaffRepo;
        private readonly IStaffSkillRepository _staffSkillRepo;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepo,
            IShiftAssignmentRepository shiftAssignmentRepo,
            IStoreStaffRepository storeStaffRepo,
            IStaffSkillRepository staffSkillRepo,
            IEmailSender emailSender,
            IMapper mapper)
        {
            _userRepo = userRepo;
            _shiftAssignmentRepo = shiftAssignmentRepo;
            _storeStaffRepo = storeStaffRepo;
            _staffSkillRepo = staffSkillRepo;
            _emailSender = emailSender;
            _mapper = mapper;
        }

        public async Task DeleteUserAsync(string username)
        {
            var user = await _userRepo
                .GetUserByUsernameAsync(username);

            if (user == null)
                throw new AppException(400, "User not found or have been deleted");

            IEnumerable<StoreStaff> storeStaffs = await _storeStaffRepo
                .GetStoresFromStaffAsync(user.Username);

            foreach (var storeStaff in storeStaffs)
            {
                _storeStaffRepo.Delete(storeStaff);
            }

            _userRepo.Delete(user);

            if (await _userRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not delete user");
        }

        public async Task<PagedList<UserOverview>> GetBrandManagersAsync(UserParams @params)
        {
            return await _userRepo.GetBrandManagersAsync(@params);
        }

        public async Task<PagedList<UserOverview>> GetStaff(UserParams @params)
        {
            return await _userRepo.GetStaffAsync(@params);
        }

        public async Task<PagedList<UserOverview>> GetStaffAsync(
            int brandId, UserParams @params)
        {
            return await _userRepo.GetStaffAsync(brandId, @params);
        }

        public async Task<PagedList<UserOverview>> GetStoreManagersAsync(
            int brandId, UserParams @params)
        {
            return await _userRepo.GetStoreManagersAsync(brandId, @params);
        }

        public async Task<UserInfoResponse> GetUserAsync(string username)
        {
            var user = await _userRepo.GetByUsernameAsync(username);

            if (user != null)
                return user;

            throw new AppException(400, "Username not found");
        }

        public async Task<UserGeneralResponse> GetUserGeneralAsync(string username)
        {
            UserGeneralResponse result = new();
            result.GeneralInfo = await GetUserAsync(username);
            result.JobInformations = await _storeStaffRepo
                .GetStoreOverviewsFromStaffAsync(username);
            result.StaffSkills = await _staffSkillRepo
                .GetSkillsFromStaffAsync(username);

            return result;
        }

        public async Task<PagedList<UserOverview>> GetUsersAsync(UserParams @params)
        {
            return await _userRepo.GetUsersAsync(@params);
        }

        public async Task<PagedList<UserOverview>> GetUsersAsync(int brandId,
            UserParams @params)
        {
            return await _userRepo.GetUsersAsync(brandId, @params);
        }

        public async Task<WorkHoursResponse> GetWorkHoursResponse(
            string username, DateTimeParams @params)
        {
            WorkHoursResponse result = new();
            var shiftAssignments = await _shiftAssignmentRepo
                .GetShiftAssignmentOverviewsAsync(username, @params);

            foreach (var shiftAssignment in shiftAssignments)
            {
                TimeSpan hoursAssigned =
                    shiftAssignment.TimeEnd - shiftAssignment.TimeStart;
                
                if (shiftAssignment.TimeCheckOut > DateTime.MinValue 
                    && shiftAssignment.TimeCheckIn > DateTime.MinValue)
                {
                    TimeSpan hoursWorkd =
                        shiftAssignment.TimeCheckOut - shiftAssignment.TimeCheckIn;
                    result.HoursWorked += hoursWorkd.TotalHours;
                }
                result.HoursAssigned += hoursAssigned.TotalHours;
            }

            return result;
        }

        public async Task RestorePasswordAsync(string username)
        {
            var user = await _userRepo.GetUserByUsernameAsync(username);

            if (user == null)
                throw new AppException(400, "User not found");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var newPassword = Helper.GenerateRandomPassword(6);
            var computedHash = hmac
                .ComputeHash(Encoding.UTF8.GetBytes(newPassword));

            user.Password = computedHash;
            _userRepo.Update(user);

            if (await _userRepo.SaveChangesAsync())
            {
                await _emailSender.SendEmailAsync(new MailMessage(
                    new string[] { user.Email },
                    "STS account restore",
                    "<p>Your new password for username: " + user.Username + "</p>" +
                    "Is: " + newPassword));
                return;
            }

            throw new AppException(400, "Can not restore password");
        }

        public async Task UpdatePasswordAsync(
            string username, PasswordUpdate update)
        {
            var user = await _userRepo.GetUserByUsernameAsync(username);

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac
                .ComputeHash(Encoding.UTF8.GetBytes(update.CurrentPassword));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.Password[i])
                    throw new AppException(
                        (int)StatusCode.UnAuthorized, "Current Password Incorrect");
            }

            computedHash = hmac
                .ComputeHash(Encoding.UTF8.GetBytes(update.NewPassword));

            user.Password = computedHash;
            _userRepo.Update(user);

            if (await _userRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not update password");
        }

        public async Task UpdateUserAsync(string username,
            UserUpdate updateInfo)
        {
            var user = await _userRepo.GetUserByUsernameAsync(username);

            _mapper.Map(updateInfo, user);
            _userRepo.Update(user);

            if (await _userRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not update user");
        }

        public async Task UpdateUserAsync(
            StaffUpdateRequest updateInfo)
        {
            var user = await _userRepo
                .GetUserByUsernameAsync(updateInfo.Username);

            _mapper.Map(updateInfo, user);
            _userRepo.Update(user);

            if (await _userRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not update user");
        }
    }
}
