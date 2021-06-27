﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class WeekScheduleRepository : BaseRepository<WeekSchedule>,
        IWeekScheduleRepository
    {
        private readonly IMapper _mapper;

        public WeekScheduleRepository(DataContext context,
            IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public override async Task<WeekSchedule> GetByIdAsync(int id)
        {
            var result = await _entities
                .Include(x => x.WeekScheduleDetails)
                .Include(x => x.StaffScheduleDetails)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result?.Status == Enums.Status.Active)
                return result;

            return null;
        }

        public async Task<PagedList<WeekScheduleOverview>> GetWeekSchedulesAsync(
            int storeId, WeekScheduleParams @params)
        {
            var source = _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.StoreId == storeId)
                .OrderByDescending(ws => ws.DateStart)
                .ProjectTo<WeekScheduleOverview>(_mapper.ConfigurationProvider);

            return await PagedList<WeekScheduleOverview>
                .CreateAsync(source, @params.PageNumber, @params.PageSize);
        }
    }
}
