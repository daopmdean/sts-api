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
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        private readonly IMapper _mapper;

        public PostRepository(DataContext context,
            IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedList<PostOverview>> GetPostsAsync(int brandId,
            PostParams @params)
        {
            var source = _entities
                .Where(s => s.Status == Enums.Status.Active)
                .Where(s => s.BrandId == brandId)
                .OrderByDescending(s => s.CreatedDate)
                .ProjectTo<PostOverview>(_mapper.ConfigurationProvider);

            return await PagedList<PostOverview>
                .CreateAsync(source, @params.PageNumber, @params.PageSize);
        }
    }
}
