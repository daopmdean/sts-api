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
    public class WeekScheduleDetailService : IWeekScheduleDetailService
    {
        private readonly IWeekScheduleRepository _weekScheduleRepo;
        private readonly IWeekScheduleDetailRepository _weekScheduleDetailRepo;
        private readonly ISkillRepository _skillRepo;
        private readonly IMapper _mapper;

        public WeekScheduleDetailService(
            IWeekScheduleRepository weekScheduleRepo,
            IWeekScheduleDetailRepository weekScheduleDetailRepo,
            ISkillRepository skillRepo,
            IMapper mapper)
        {
            _weekScheduleRepo = weekScheduleRepo;
            _weekScheduleDetailRepo = weekScheduleDetailRepo;
            _skillRepo = skillRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WeekScheduleDetail>> CreateWeekScheduleDetailAsync(
            IEnumerable<WeekScheduleDetailCreate> creates)
        {
            List<WeekScheduleDetail> result = new();
            foreach (var create in creates)
            {
                var weekSchedule = await _weekScheduleRepo
                    .GetByIdAsync(create.WeekScheduleId);

                if (weekSchedule == null)
                    throw new AppException(400,
                        "Conflicted with the FOREIGN KEY constraint, WeekScheduleId does not exist");

                var skill = await _skillRepo
                    .GetByIdAsync(create.SkillId);

                if (skill == null)
                    throw new AppException(400,
                        "Conflicted with the FOREIGN KEY constraint, SkillId does not exist");

                var weekScheduleDetail = _mapper.Map<WeekScheduleDetail>(create);
                await _weekScheduleDetailRepo.CreateAsync(weekScheduleDetail);
                result.Add(weekScheduleDetail);
            }

            if (await _weekScheduleDetailRepo.SaveChangesAsync())
                return result;

            throw new AppException(400, "Can not create week schedule detail");
        }

        public async Task DeleteWeekScheduleDetailAsync(int id)
        {
            var weekScheduleDetail = await _weekScheduleDetailRepo
                .GetByIdAsync(id);

            if (weekScheduleDetail == null)
                throw new AppException(400, "WeekScheduleDetail not found");

            _weekScheduleDetailRepo.Delete(weekScheduleDetail);

            if (await _weekScheduleDetailRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not delete WeekScheduleDetail");
        }

        public async Task<WeekScheduleDetail> GetWeekScheduleDetail(int id)
        {
            var weekScheduleDetail = await _weekScheduleDetailRepo
                .GetByIdAsync(id);

            if (weekScheduleDetail == null)
                throw new AppException(400,
                    "WeekScheduleDetail not found or has been deleted");

            return weekScheduleDetail;
        }

        public async Task<IEnumerable<WeekScheduleDetail>> GetWeekScheduleDetailsAsync(
            int weekScheduleId)
        {
            return await _weekScheduleDetailRepo
                .GetWeekScheduleDetailsAsync(weekScheduleId);
        }

        public async Task UpdateWeekScheduleDetailAsync(int id,
            WeekScheduleDetailUpdate update)
        {
            var weekScheduleDetail = await _weekScheduleDetailRepo
                .GetByIdAsync(id);

            if (weekScheduleDetail == null)
                throw new AppException(400, "WeekScheduleDetail not found");

            _mapper.Map(update, weekScheduleDetail);

            _weekScheduleDetailRepo.Update(weekScheduleDetail);

            if (await _weekScheduleDetailRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not update WeekScheduleDetail");
        }
    }
}
