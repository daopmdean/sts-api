using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Data.Models.Requests;
using Data.Repositories.Interfaces;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations
{
    public class StoreScheduleDetailService : IStoreScheduleDetailService
    {
        private readonly IWeekScheduleRepository _weekScheduleRepo;
        private readonly IStoreScheduleDetailRepository _storeScheduleDetailRepo;
        private readonly IMapper _mapper;

        public StoreScheduleDetailService(
            IWeekScheduleRepository weekScheduleRepo,
            IStoreScheduleDetailRepository storeScheduleRepo,
            IMapper mapper)
        {
            _storeScheduleDetailRepo = storeScheduleRepo;
            _weekScheduleRepo = weekScheduleRepo;
            _mapper = mapper;
        }

        public async Task<StoreScheduleDetail> CreateStoreScheduleDetailAsync(
            StoreScheduleDetailCreate create)
        {
            var weekSchedule = await _weekScheduleRepo
                .GetByIdAsync(create.WeekScheduleId);

            if (weekSchedule == null)
                throw new AppException(400,
                    "Conflicted with the FOREIGN KEY constraint, WeekScheduleId does not exist");

            var storeScheduleDetail = _mapper.Map<StoreScheduleDetail>(create);
            await _storeScheduleDetailRepo.CreateAsync(storeScheduleDetail);

            if (await _storeScheduleDetailRepo.SaveChangesAsync())
                return storeScheduleDetail;

            throw new AppException(400, "Can not create store schedule detail");
        }

        public async Task DeleteStoreScheduleDetailAsync(int id)
        {
            var storeSchedule = await _storeScheduleDetailRepo
                .GetByIdAsync(id);

            if (storeSchedule == null)
                throw new AppException(400, "StoreScheduleDetail not found");

            _storeScheduleDetailRepo.Delete(storeSchedule);

            if (await _storeScheduleDetailRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not delete StoreScheduleDetail");
        }

        public async Task<StoreScheduleDetail> GetStoreScheduleDetail(int id)
        {
            var storeScheduleDetail = await _storeScheduleDetailRepo
                .GetByIdAsync(id);

            if (storeScheduleDetail == null)
                throw new AppException(400,
                    "StoreScheduleDetail not found or has been deleted");

            return storeScheduleDetail;
        }

        public async Task<IEnumerable<StoreScheduleDetail>> GetStoreScheduleDetails(
            int weekScheduleId)
        {
            return await _storeScheduleDetailRepo
                .GetStoreScheduleDetailsAsync(weekScheduleId);
        }

        public async Task UpdateStoreScheduleDetailAsync(int id,
            StoreScheduleDetailUpdate update)
        {
            var storeScheduleDetail = await _storeScheduleDetailRepo
                .GetByIdAsync(id);

            if (storeScheduleDetail == null)
                throw new AppException(400, "StoreScheduleDetail not found");

            _mapper.Map(update, storeScheduleDetail);

            _storeScheduleDetailRepo.Update(storeScheduleDetail);

            if (await _storeScheduleDetailRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not update StoreScheduleDetail");
        }
    }
}
