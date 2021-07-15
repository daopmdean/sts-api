﻿using AutoMapper;
using Data.Entities;
using Data.Models.Responses;
using Data.Repositories.Interfaces;
using Service.Exceptions;
using Service.Interfaces;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class ShiftScheduleResultService : IShiftScheduleResultService
    {
        private readonly IShiftScheduleResultRepository _scheduleRepo;
        private readonly IShiftScheduleDetailResultRepository _scheduleDetailRepo;
        private readonly IMapper _mapper;
        public ShiftScheduleResultService(
            IShiftScheduleResultRepository scheduleRepo,
            IShiftScheduleDetailResultRepository scheduleDetailRepo,
            IMapper mapper)
        {
            _scheduleRepo = scheduleRepo;
            _scheduleDetailRepo = scheduleDetailRepo;
            _mapper = mapper;
        }

        public async Task<ShiftScheduleResult> CreateShiftScheduleResult()
        {
            var shiftScheduleResult = new ShiftScheduleResult();
            await _scheduleRepo
                .CreateAsync(shiftScheduleResult);

            if (await _scheduleRepo.SaveChangesAsync())
                return shiftScheduleResult;

            throw new AppException(400, "Can not create ShiftScheduleResult");
        }

        public async Task<ScheduleResponse> GetScheduleResult(long id)
        {
            var shiftScheduleResult = await CheckShiftScheduleResult(id);
            var result = _mapper.Map<ScheduleResponse>(shiftScheduleResult);
            var shiftAssignments = await _scheduleDetailRepo
                .GetShiftAssignments(id);
            result.ShiftAssignments = shiftAssignments;
            return result;
        }

        public async Task<ShiftScheduleResult> CheckShiftScheduleResult(long id)
        {
            var shiftScheduleResult = await _scheduleRepo
                .GetByIdAsync((int)id);

            if (shiftScheduleResult == null)
                throw new AppException(400,
                    "ShiftScheduleResult not found or has been deleted");

            return shiftScheduleResult;
        }

        public async Task CreateShiftScheduleResult(ScheduleResponse create)
        {
            var shiftAssignments = create.ShiftAssignments;

            if (shiftAssignments != null)
            { 
                foreach (var shiftAssignment in shiftAssignments)
                {
                    var shiftScheduleDetailResult = _mapper
                        .Map<ShiftScheduleDetailResult>(shiftAssignment);
                    shiftScheduleDetailResult.ShiftScheduleResultId = create.ShiftScheduleResultId;
                    await _scheduleDetailRepo.CreateAsync(shiftScheduleDetailResult);
                }
            }

            //if (!await _scheduleRepo.SaveChangesAsync())
            //    throw new AppException(400, "Fail to create shift schedule detail result");

            var scheduleResult = await _scheduleRepo
                .GetByIdAsync(create.ShiftScheduleResultId);

            if (scheduleResult == null)
                throw new AppException(400, "schedule result not found");

            _mapper.Map(create, scheduleResult);
            scheduleResult.IsComplete = true;
            _scheduleRepo.Update(scheduleResult);

            if (await _scheduleRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Fail to create shift schedule result");
        }
    }
}
