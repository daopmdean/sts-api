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
    public class WeekScheduleService : IWeekScheduleService
    {
        private readonly IStoreRepository _storeRepo;
        private readonly IWeekScheduleRepository _weekRepo;
        private readonly IMapper _mapper;

        public WeekScheduleService(IStoreRepository storeRepo,
            IWeekScheduleRepository weekRepo, IMapper mapper)
        {
            _weekRepo = weekRepo;
            _storeRepo = storeRepo;
            _mapper = mapper;
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

            throw new AppException(400, "Can not create week schedule");
        }

        public async Task<WeekSchedule> GetWeekScheduleAsync(int id)
        {
            var weekSchedule = await _weekRepo.GetByIdAsync(id);

            if (weekSchedule == null)
                throw new AppException(400, "WeekSchedule not found or has been deleted");

            return weekSchedule;
        }

        public async Task<PagedList<WeekScheduleOverview>> GetWeekSchedulesAsync(
            int storeId, WeekScheduleParams @params)
        {
            return await _weekRepo.GetWeekSchedulesAsync(storeId, @params);
        }
    }
}
