using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;
using Data.Repositories.Interfaces;

namespace Data.Repositories.Implementations
{
    public class SkillRepository : BaseRepository<Skill>, ISkillRepository
    {
        private readonly IMapper _mapper;

        public SkillRepository(DataContext context,
            IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedList<SkillOverview>> GetSkillsAsync(int brandId,
            SkillParams @params)
        {
            var source = _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.BrandId == brandId)
                .OrderBy(b => b.Name)
                .ProjectTo<SkillOverview>(_mapper.ConfigurationProvider);

            return await PagedList<SkillOverview>
                .CreateAsync(source, @params.PageNumber, @params.PageSize);
        }
    }
}
