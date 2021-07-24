using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Data.Enums;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;
using Data.Repositories.Interfaces;
using Service.Enums;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations
{
    public class ShiftAssignmentService : IShiftAssignmentService
    {
        private readonly IShiftAssignmentRepository _shiftAssignmentRepo;
        private readonly IUserRepository _userRepo;
        private readonly IStoreRepository _storeRepo;
        private readonly ISkillRepository _skillRepo;
        private readonly IWeekScheduleRepository _weekScheduleRepo;
        private readonly IMapper _mapper;

        public ShiftAssignmentService(
            IShiftAssignmentRepository shiftAssignmentRepo,
            IUserRepository userRepo,
            IStoreRepository storeRepo,
            ISkillRepository skillRepo,
            IWeekScheduleRepository weekScheduleRepo,
            IMapper mapper)
        {
            _shiftAssignmentRepo = shiftAssignmentRepo;
            _userRepo = userRepo;
            _storeRepo = storeRepo;
            _skillRepo = skillRepo;
            _weekScheduleRepo = weekScheduleRepo;
            _mapper = mapper;
        }

        public async Task<ShiftAssignment> CreateShiftAssignment(
            ShiftAssignmentCreate create)
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
            }
            else
            {
                foreach (var schedule in publishedWeekSchedule)
                {
                    schedule.Status = Status.Unpublished;
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

        public async Task DeleteShiftAssignment(int id)
        {
            var shiftAssignment = await _shiftAssignmentRepo
                .GetByIdAsync(id);

            if (shiftAssignment == null)
                throw new AppException(400, "ShiftAssignment not found");

            _shiftAssignmentRepo.Delete(shiftAssignment);

            if (await _shiftAssignmentRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not delete ShiftAssignment");
        }

        public async Task<ShiftAssignment> GetShiftAssignment(int id)
        {
            var shiftAssignment = await _shiftAssignmentRepo
                .GetByIdAsync(id);

            if (shiftAssignment == null)
                throw new AppException(400,
                    "ShiftAssignment not found or has been deleted");

            return shiftAssignment;
        }

        public async Task<PagedList<ShiftAssignmentOverview>> GetShiftAssignments(
            string username, ShiftAssignmentParams @params)
        {
            return await _shiftAssignmentRepo
                .GetShiftAssignmentsAsync(username, @params);
        }

        public async Task<PagedList<ShiftAssignmentOverview>> GetShiftAssignments(
            int storeId, ShiftAssignmentParams @params)
        {
            return await _shiftAssignmentRepo
                .GetShiftAssignmentsAsync(storeId, @params);
        }

        public async Task UpdateShiftAssignment(int id,
            ShiftAssignmentUpdate update)
        {
            var shiftAssignment = await _shiftAssignmentRepo
                .GetByIdAsync(id);

            if (shiftAssignment == null)
                throw new AppException(400, "ShiftAssignment not found");

            _mapper.Map(update, shiftAssignment);

            _shiftAssignmentRepo.Update(shiftAssignment);

            if (await _shiftAssignmentRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not update ShiftAssignment");
        }
    }
}
