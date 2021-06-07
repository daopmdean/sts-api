using System;
using AutoMapper;
using Data.Entities;
using Data.Repositories.Interfaces;

namespace Data.Repositories.Implementations
{
    public class WeekScheduleRepository : BaseRepository<WeekSchedule>,
        IWeekScheduleRepository
    {
        private readonly IMapper _mapper;

        public WeekScheduleRepository(DataContext context,
            IMapper mapper) : base(context)
        {
        }
    }
}
