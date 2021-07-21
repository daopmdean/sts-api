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
        private readonly IMapper _mapper;

        public WeekScheduleService(
            IStoreRepository storeRepo,
            IWeekScheduleRepository weekRepo,
            IWeekScheduleDetailRepository weekDetailRepo,
            IMapper mapper)
        {
            _weekRepo = weekRepo;
            _storeRepo = storeRepo;
            _weekDetailRepo = weekDetailRepo;
            _mapper = mapper;
        }

        public async Task<WeekSchedule> CloneWeekScheduleAsync(
            WeekScheduleCloneRequest cloneRequest)
        {
            var baseWeekSchedule = await _weekRepo
                .GetByIdAsync(cloneRequest.WeekScheduleId);
            var cloneWeekSchedule = baseWeekSchedule.ShallowClone();

            await _weekRepo.CreateAsync(cloneWeekSchedule);

            var baseWeekScheduleDetails = await _weekDetailRepo
                .GetWeekScheduleDetailsAsync(baseWeekSchedule.Id);

            foreach (var baseWeekScheduleDetail in baseWeekScheduleDetails)
            {
                var cloneWeekScheduleDetail = baseWeekScheduleDetail
                    .ShallowClone();


            }



            if (await _weekRepo.SaveChangesAsync())
                return cloneWeekSchedule;

            throw new AppException((int)StatusCode.BadRequest,
                "Can not clone week schedule");
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
            int storeId, DateTime dateStart, Status weekStatus)
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
                        var res = await CreateWeekScheduleAsync(new WeekScheduleCreate
                        {
                            StoreId = storeId,
                            DateStart = dateStart,
                            Status = Status.Register
                        });
                        result.Add(res);
                        break;
                    case Status.Unpublished:
                        var unp = await CreateWeekScheduleAsync(new WeekScheduleCreate
                        {
                            StoreId = storeId,
                            DateStart = dateStart,
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
    }
}
