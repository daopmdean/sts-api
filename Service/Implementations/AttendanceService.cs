using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Data.Enums;
using Data.Models.Requests;
using Data.Pagings;
using Data.Repositories.Interfaces;
using Service.Enums;
using Service.Exceptions;
using Service.Helpers;
using Service.Interfaces;

namespace Service.Implementations
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepo;
        private readonly IStoreRepository _storeRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public AttendanceService(
            IAttendanceRepository attendanceRepo,
            IStoreRepository storeRepo,
            IUserRepository userRepo,
            IMapper mapper)
        {
            _attendanceRepo = attendanceRepo;
            _storeRepo = storeRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<Attendance> CreateAttendanceAsync(
            AttendanceCreate create)
        {
            var store = await _storeRepo
                .GetByIdAsync(create.StoreId);

            if (store == null)
                throw new AppException((int)StatusCode.BadRequest,
                    "Store not found");

            var user = await _userRepo
                .GetUserByUsernameAsync(create.Username);

            if (user == null)
                throw new AppException((int)StatusCode.BadRequest,
                    "User not found");

            var attendance = _mapper.Map<Attendance>(create);
            await _attendanceRepo.CreateAsync(attendance);

            if (await _attendanceRepo.SaveChangesAsync())
            {
                await FCMNotification
                    .SendNotificationAsync(attendance.Username,
                    "Attendance Recorded",
                    "Your attendance had been recorded");
                return attendance;
            }


            throw new AppException(400, "Can not create Attendance");
        }

        public async Task<Attendance> CreateAttendanceManualAsync(
            string username, AttendanceManualCreate create)
        {
            var store = await _storeRepo
                .GetByIdAsync(create.StoreId);

            if (store == null)
                throw new AppException((int)StatusCode.BadRequest,
                    "Store not found");

            var user = await _userRepo
                .GetUserByUsernameAsync(create.Username);

            if (user == null)
                throw new AppException((int)StatusCode.BadRequest,
                    "User not found");

            var attendance = _mapper.Map<Attendance>(create);
            attendance.CheckType = CheckType.Manual;
            attendance.CreatedBy = username;
            await _attendanceRepo.CreateAsync(attendance);

            if (await _attendanceRepo.SaveChangesAsync())
                return attendance;

            throw new AppException(400, "Can not create Attendance");
        }

        public async Task DeleteAttendanceAsync(int id)
        {
            var attendance = await _attendanceRepo
                .GetByIdAsync(id);

            if (attendance == null)
                throw new AppException(400, "Attendance not found");

            _attendanceRepo.Delete(attendance);

            if (await _attendanceRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not delete Attendance");
        }

        public async Task<Attendance> GetAttendanceAsync(int id)
        {
            var attendance = await _attendanceRepo
                .GetByIdAsync(id);

            if (attendance == null)
                throw new AppException(400,
                    "Attendance not found or has been deleted");

            return attendance;
        }

        public async Task<IEnumerable<Attendance>> GetAttendancesAsync(
            string username, DateTimeParams @params)
        {
            return await _attendanceRepo
                .GetAttendancesAsync(username, @params);
        }

        public async Task<IEnumerable<Attendance>> GetAttendancesAsync(
            int storeId, DateTimeParams @params)
        {
            return await _attendanceRepo
                .GetAttendancesAsync(storeId, @params);
        }

        public async Task UpdateAttendanceAsync(
            int id, AttendanceUpdate update)
        {
            var attendance = await _attendanceRepo
                .GetByIdAsync(id);

            if (attendance == null)
                throw new AppException(400, "Attendance not found");

            _mapper.Map(update, attendance);

            _attendanceRepo.Update(attendance);

            if (await _attendanceRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not update Attendance");
        }
    }
}
