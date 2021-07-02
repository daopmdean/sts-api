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
    public class StaffScheduleDetailService : IStaffScheduleDetailService
    {
        private readonly IWeekScheduleRepository _weekScheduleRepo;
        private readonly IStaffScheduleDetailRepository _staffScheduleDetailRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public StaffScheduleDetailService(
            IWeekScheduleRepository weekScheduleRepo,
            IStaffScheduleDetailRepository staffScheduleRepo,
            IUserRepository userRepo,
            IMapper mapper)
        {
            _staffScheduleDetailRepo = staffScheduleRepo;
            _weekScheduleRepo = weekScheduleRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StaffScheduleDetailCreate>> CreateStaffScheduleDetailAsync(
            IEnumerable<StaffScheduleDetailCreate> creates)
        {
            foreach (var create in creates)
            {
                var weekSchedule = await _weekScheduleRepo
                .GetByIdAsync(create.WeekScheduleId);

                if (weekSchedule == null)
                    throw new AppException(400,
                        "Conflicted with the FOREIGN KEY constraint, WeekScheduleId does not exist");

                var user = await _userRepo
                    .GetUserByUsernameAsync(create.Username);

                if (user == null)
                    throw new AppException(400,
                        "Conflicted with the FOREIGN KEY constraint, Username does not exist");

                var staffScheduleDetail = _mapper.Map<StaffScheduleDetail>(create);
                await _staffScheduleDetailRepo.CreateAsync(staffScheduleDetail);
            }

            if (await _staffScheduleDetailRepo.SaveChangesAsync())
                return creates;

            throw new AppException(400, "Can not create staff schedule detail");
        }

        public async Task DeleteStaffScheduleDetailAsync(int id)
        {
            var staffScheduleDetail = await _staffScheduleDetailRepo
                .GetByIdAsync(id);

            if (staffScheduleDetail == null)
                throw new AppException(400, "StaffScheduleDetail not found");

            _staffScheduleDetailRepo.Delete(staffScheduleDetail);

            if (await _staffScheduleDetailRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not delete StaffScheduleDetail");
        }

        public async Task<StaffScheduleDetail> GetStaffScheduleDetail(int id)
        {
            var staffScheduleDetail = await _staffScheduleDetailRepo
                .GetByIdAsync(id);

            if (staffScheduleDetail == null)
                throw new AppException(400,
                    "StaffScheduleDetail not found or has been deleted");

            return staffScheduleDetail;
        }

        public async Task<PagedList<StaffScheduleDetailOverview>> GetStaffScheduleDetails(
            int weekScheduleId, StaffScheduleDetailParams @params)
        {
            return await _staffScheduleDetailRepo
                .GetStaffScheduleDetailsAsync(weekScheduleId, @params);
        }

        public async Task UpdateStaffScheduleDetailAsync(int id,
            StaffScheduleDetailUpdate update)
        {
            var staffScheduleDetail = await _staffScheduleDetailRepo
                .GetByIdAsync(id);

            if (staffScheduleDetail == null)
                throw new AppException(400, "StaffScheduleDetail not found");

            _mapper.Map(update, staffScheduleDetail);

            _staffScheduleDetailRepo.Update(staffScheduleDetail);

            if (await _staffScheduleDetailRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not update StaffScheduleDetail");
        }
    }
}
