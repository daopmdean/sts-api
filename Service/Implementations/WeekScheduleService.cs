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
    public class WeekScheduleService : IWeekScheduleService
    {
        private readonly IStoreRepository _storeRepo;
        private readonly IWeekScheduleRepository _weekRepo;
        private readonly IWeekScheduleDetailRepository _weekDetailRepo;
        private readonly IStoreScheduleDetailRepository _storeScheduleRepo;
        private readonly IShiftScheduleResultRepository _shiftScheduleRepo;
        private readonly IShiftScheduleDetailResultRepository _shiftScheduleDetailRepo;
        private readonly IMapper _mapper;

        public WeekScheduleService(
            IStoreRepository storeRepo,
            IWeekScheduleRepository weekRepo,
            IWeekScheduleDetailRepository weekDetailRepo,
            IStoreScheduleDetailRepository storeScheduleRepo,
            IShiftScheduleResultRepository shiftScheduleRepo,
            IShiftScheduleDetailResultRepository shiftScheduleDetailRepo,
            IMapper mapper)
        {
            _weekRepo = weekRepo;
            _storeRepo = storeRepo;
            _weekDetailRepo = weekDetailRepo;
            _storeScheduleRepo = storeScheduleRepo;
            _shiftScheduleRepo = shiftScheduleRepo;
            _shiftScheduleDetailRepo = shiftScheduleDetailRepo;
            _mapper = mapper;
        }

        public async Task<WeekSchedule> CloneWeekScheduleAsync(
            WeekScheduleCloneRequest cloneRequest)
        {
            var baseWeekSchedule = await _weekRepo
                .GetByIdAsync(cloneRequest.WeekScheduleId);
            var cloneWeekSchedule = baseWeekSchedule.ShallowClone();

            await _weekRepo.CreateAsync(cloneWeekSchedule);

            //--------------------------------
            if (await _weekRepo.SaveChangesAsync())
            {
                Console.WriteLine(cloneWeekSchedule.Id);
            }
            else
            {
                throw new AppException((int)StatusCode.BadRequest,
                    "CloneWeekScheduleAsync: Can not clone WeekSchedule");
            }
            //--------------------------------

            var baseWeekScheduleDetails = await _weekDetailRepo
                .GetWeekScheduleDetailsAsync(baseWeekSchedule.Id);
            foreach (var baseWeekScheduleDetail in baseWeekScheduleDetails)
            {
                var cloneWeekScheduleDetail = _mapper
                    .Map<WeekScheduleDetail>(baseWeekScheduleDetail);
                cloneWeekScheduleDetail.Id = 0;
                cloneWeekScheduleDetail.WeekScheduleId = cloneWeekSchedule.Id;

                await _weekDetailRepo.CreateAsync(cloneWeekScheduleDetail);
            }

            //--------------------------------
            if (await _weekRepo.SaveChangesAsync())
            {
                Console.WriteLine(cloneWeekSchedule.Id);
            }
            else
            {
                throw new AppException((int)StatusCode.BadRequest,
                    "CloneWeekScheduleAsync: Can not clone WeekScheduleDetail");
            }
            //--------------------------------

            var baseStoreScheduleDetails = await _storeScheduleRepo
                .GetStoreScheduleDetailsAsync(baseWeekSchedule.Id);
            foreach (var baseStoreScheduleDetail in baseStoreScheduleDetails)
            {
                var cloneStoreScheduleDetail = _mapper
                    .Map<StoreScheduleDetail>(baseStoreScheduleDetail);
                cloneStoreScheduleDetail.Id = 0;
                cloneStoreScheduleDetail.WeekScheduleId = cloneWeekSchedule.Id;

                await _storeScheduleRepo.CreateAsync(cloneStoreScheduleDetail);
            }

            //--------------------------------
            if (await _weekRepo.SaveChangesAsync())
            {
                return cloneWeekSchedule;
            }
            else
            {
                throw new AppException((int)StatusCode.BadRequest,
                    "CloneWeekScheduleAsync: Can not clone StoreScheduleDetail");
            }
            //--------------------------------

            //var shiftScheduleResult = new ShiftScheduleResult
            //{
            //    IsComplete = true,
            //    WeekScheduleId = cloneWeekSchedule.Id
            //};
            //await _shiftScheduleRepo.CreateAsync(shiftScheduleResult);

            //var shiftAssignments = cloneRequest.ShiftAssignments;

            //if (shiftAssignments != null)
            //{
            //    foreach (var shiftAssignment in shiftAssignments)
            //    {
            //        var assignment = _mapper.Map<ShiftScheduleDetailResult>(shiftAssignment);
            //        assignment.ShiftScheduleResultId = shiftScheduleResult.Id;
            //        await _shiftScheduleDetailRepo.CreateAsync(assignment);
            //    }
            //}
        }

        public async Task<WeekSchedule> CreateWeekScheduleAsync(
            WeekScheduleCreate weekScheduleCreate)
        {
            var store = await _storeRepo.GetByIdAsync(weekScheduleCreate.StoreId);

            if (store == null)
                throw new AppException(400,
                    "Conflicted with the FOREIGN KEY constraint, storeId does not exist");

            var weekSchedule = _mapper.Map<WeekSchedule>(weekScheduleCreate);
            await _weekRepo.CreateAsync(weekSchedule);

            if (await _weekRepo.SaveChangesAsync())
                return weekSchedule;

            throw new AppException((int)StatusCode.BadRequest,
                "Can not create week schedule");
        }

        public async Task DeleteWeekScheduleAsync(int id)
        {
            var weekSchedule = await _weekRepo
                .GetByIdAsync(id);

            if (weekSchedule == null)
                throw new AppException(400, "WeekSchedule not found");

            _weekRepo.Delete(weekSchedule);

            if (await _weekRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not delete WeekSchedule");
        }

        public async Task<WeekSchedule> GetWeekScheduleAsync(int id)
        {
            var weekSchedule = await _weekRepo.GetByIdAsync(id);

            if (weekSchedule == null)
                throw new AppException((int)StatusCode.BadRequest,
                    "WeekSchedule not found or has been deleted");

            return weekSchedule;
        }

        public async Task<IEnumerable<WeekSchedule>> GetWeekScheduleAsync(
            int storeId, DateTime dateStart, Status weekStatus, string createdBy)
        {
            var weekSchedule = await _weekRepo
                .GetWeekSchedulesAsync(storeId, dateStart, weekStatus);

            if (weekSchedule == null)
            {
                List<WeekSchedule> result = new();
                Data.Helpers.Helper.TransformDateStart(ref dateStart);
                switch (weekStatus)
                {
                    case Status.Register:
                        var reg = await CreateWeekScheduleAsync(new WeekScheduleCreate
                        {
                            StoreId = storeId,
                            DateStart = dateStart,
                            CreatedBy = createdBy,
                            Status = Status.Register
                        });
                        result.Add(reg);
                        break;
                    case Status.Unpublished:
                        var unp = await CreateWeekScheduleAsync(new WeekScheduleCreate
                        {
                            StoreId = storeId,
                            DateStart = dateStart,
                            CreatedBy = createdBy,
                            Status = Status.Unpublished
                        });
                        result.Add(unp);
                        break;
                }
                weekSchedule = result;
            }

            return weekSchedule;
        }

        public async Task<PagedList<WeekScheduleOverview>> GetWeekSchedulesAsync(
            int storeId, WeekScheduleParams @params)
        {
            return await _weekRepo.GetWeekSchedulesAsync(storeId, @params);
        }

        public async Task UpdateWeekScheduleAsync(
            int id, WeekScheduleUpdate update)
        {
            var weekSchedule = await _weekRepo
                .GetByIdAsync(id);

            if (weekSchedule == null)
                throw new AppException(400, "WeekSchedule not found");

            _mapper.Map(update, weekSchedule);

            _weekRepo.Update(weekSchedule);

            if (await _weekRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not update WeekSchedule");
        }
    }
}
