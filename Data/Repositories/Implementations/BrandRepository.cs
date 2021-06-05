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
    public class BrandRepository : BaseRepository<Brand>, IBrandRepository
    {
        private readonly IMapper _mapper;

        public BrandRepository(DataContext context, IMapper mapper)
            : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedList<BrandOverview>> GetBrandsAsync(
            BrandParams @params)
        {
            var source = _entities
                .Where(b => b.Status == Enums.Status.Active)
                .OrderBy(b => b.Name)
                .ProjectTo<BrandOverview>(_mapper.ConfigurationProvider);

            return await PagedList<BrandOverview>
                .CreateAsync(source, @params.PageNumber, @params.PageSize);
        }
    }
}
