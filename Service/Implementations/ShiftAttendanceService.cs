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
using Service.Helpers;
using Service.Interfaces;

namespace Service.Implementations
{
    public class ShiftAttendanceService : IShiftAttendanceService
    {
        private readonly IShiftAttendanceRepository _shiftAttendanceRepo;
        private readonly IShiftAssignmentRepository _shiftAssignmentRepo;
        private readonly IStoreStaffService _storeStaffService;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public ShiftAttendanceService(
            IShiftAttendanceRepository shiftAttendanceRepo,
            IShiftAssignmentRepository shiftAssignmentRepo,
            IStoreStaffService storeStaffService,
            IUserRepository userRepo,
            IMapper mapper)
        {
            _shiftAttendanceRepo = shiftAttendanceRepo;
            _shiftAssignmentRepo = shiftAssignmentRepo;
            _storeStaffService = storeStaffService;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<ShiftAttendance> CreateShiftAttendance(
            ShiftAttendanceCreate create, int timeRange)
        {
            var shiftAssignment = await _shiftAssignmentRepo
                .GetShiftAssignmentAsync(create.Username,
                    create.TimeRequest, timeRange);

            if (shiftAssignment == null)
                throw new AppException(400,
                    "Can not create ShiftAttendance - ShiftAssignment does not exist");

            var shiftAttendance = await _shiftAttendanceRepo
                .GetByIdAsync(shiftAssignment.Id);

            if (shiftAttendance == null)
            {
                shiftAttendance = new()
                {
                    ShiftAssignmentId = shiftAssignment.Id,
                };
                ProcessShiftAttendance(ref shiftAttendance,
                    shiftAssignment, create.TimeRequest, timeRange, create.Type);

                await _shiftAttendanceRepo.CreateAsync(shiftAttendance);
            }
            else
            {
                ProcessShiftAttendance(ref shiftAttendance,
                    shiftAssignment, create.TimeRequest, timeRange, create.Type);
                _shiftAttendanceRepo.Update(shiftAttendance);
            }

            if (await _shiftAttendanceRepo.SaveChangesAsync())
                return shiftAttendance;

            throw new AppException(400,
                "Error at ShiftAttendanceService.cs - CreateShiftAttendance()");
        }

        private static void ProcessShiftAttendance(
            ref ShiftAttendance shiftAttendance,
            ShiftAssignment shiftAssignment,
            DateTime timeRequest, int timeRange, string type)
        {
            type = type.ToLower();

            if (Helper.InTimeRange(shiftAssignment.TimeStart,
                        timeRequest, timeRange) && type == "checkin")
            {
                if (shiftAttendance.TimeCheckIn == DateTime.MinValue)
                    shiftAttendance.TimeCheckIn = timeRequest;
            }
            else if (Helper.InTimeRange(shiftAssignment.TimeEnd,
                        timeRequest, timeRange) && type == "checkout")
            {
                shiftAttendance.TimeCheckOut = timeRequest;
            }
        }

        public async Task DeleteShiftAttendance(int id)
        {
            var shiftAttendance = await _shiftAttendanceRepo
                .GetByIdAsync(id);

            if (shiftAttendance == null)
                throw new AppException(400, "ShiftAttendance not found");

            _shiftAttendanceRepo.Delete(shiftAttendance);

            if (await _shiftAttendanceRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not delete ShiftAttendance");
        }

        public async Task<ShiftAttendance> GetShiftAttendance(int id)
        {
            var shiftAttendance = await _shiftAttendanceRepo
                .GetByIdAsync(id);

            if (shiftAttendance == null)
                throw new AppException(400,
                    "ShiftAttendance not found or has been deleted");

            return shiftAttendance;
        }

        public async Task<PagedList<ShiftAttendanceOverview>> GetShiftAttendencesAsync(
            string username, ShiftAttendanceParams @params)
        {
            return await _shiftAttendanceRepo
                .GetShiftAttendancesAsync(username, @params);
        }

        public async Task<IEnumerable<ShiftAttendance>> GetShiftAttendencesAsync(
            string username, DateTimeParams @params)
        {
            return await _shiftAttendanceRepo
                .GetShiftAttendancesAsync(username, @params);
        }

        public async Task<PagedList<ShiftAttendanceOverview>> GetShiftAttendencesAsync(
            int StoreId, ShiftAttendanceParams @params)
        {
            return await _shiftAttendanceRepo
                .GetShiftAttendancesAsync(StoreId, @params);
        }

        public async Task<IEnumerable<StaffAttendancesResponse>> GetShiftAttendencesAsync(
            int storeId, DateTimeParams @params)
        {
            List<StaffAttendancesResponse> result = new();
            var storeStaffs = await _storeStaffService
                    .GetStaffFromStoreAsync(storeId);

            foreach (var staff in storeStaffs)
            {
                var user = await _userRepo.GetUserByUsernameAsync(staff.Username);
                IEnumerable<ShiftAttendance> attendances = await
                    GetShiftAttendencesAsync(user.Username, @params);
                var staffAttendanceResponse = new StaffAttendancesResponse()
                {
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Attendances = attendances
                };
                result.Add(staffAttendanceResponse);
            }

            return result;
        }

        public async Task UpdateShiftAttendance(int id,
            ShiftAttendanceUpdate update)
        {
            var shiftAttendance = await _shiftAttendanceRepo
                .GetByIdAsync(id);

            if (shiftAttendance == null)
                throw new AppException(400, "ShiftAttendance not found");

            _mapper.Map(update, shiftAttendance);

            _shiftAttendanceRepo.Update(shiftAttendance);

            if (await _shiftAttendanceRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not update ShiftAttendance");
        }
    }
}
