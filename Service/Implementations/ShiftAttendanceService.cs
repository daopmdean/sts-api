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
    public class ShiftAttendanceService : IShiftAttendanceService
    {
        private readonly IShiftAttendanceRepository _shiftAttendanceRepo;
        private readonly IShiftAssignmentRepository _shiftAssignmentRepo;
        private readonly IMapper _mapper;

        public ShiftAttendanceService(
            IShiftAttendanceRepository shiftAttendanceRepo,
            IShiftAssignmentRepository shiftAssignmentRepo,
            IMapper mapper)
        {
            _shiftAttendanceRepo = shiftAttendanceRepo;
            _shiftAssignmentRepo = shiftAssignmentRepo;
            _mapper = mapper;
        }

        public async Task<ShiftAttendance> CreateShiftAttendance(
            ShiftAttendanceCreate create)
        {
            var shiftAssignment = await _shiftAssignmentRepo
                .GetByIdAsync(create.ShiftAssignmentId);

            if (shiftAssignment == null)
                throw new AppException(400,
                    "Conflicted with the FOREIGN KEY constraint, ShiftAssignmentId does not exist");

            var shiftAttendance = _mapper.Map<ShiftAttendance>(create);
            await _shiftAttendanceRepo.CreateAsync(shiftAttendance);

            if (await _shiftAttendanceRepo.SaveChangesAsync())
                return shiftAttendance;

            throw new AppException(400, "Can not create ShiftAttendance");
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

        public async Task<PagedList<ShiftAttendanceOverview>> GetShiftAttendencesAsync(
            int StoreId, ShiftAttendanceParams @params)
        {
            return await _shiftAttendanceRepo
                .GetShiftAttendancesAsync(StoreId, @params);
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
