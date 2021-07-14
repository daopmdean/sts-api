using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Entities;
using Data.Models.Responses;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories.Implementations
{
    public class ShiftScheduleDetailResultRepository :
        BaseRepository<ShiftScheduleDetailResult>,
        IShiftScheduleDetailResultRepository
    {
        private readonly IMapper _mapper;
        public ShiftScheduleDetailResultRepository(
            DataContext context,
            IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShiftAssignmentResponse>> GetShiftAssignments(
            long shiftScheduleResultId)
        {
            return await _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.ShiftScheduleResultId == shiftScheduleResultId)
                .ProjectTo<ShiftAssignmentResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
