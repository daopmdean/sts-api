using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;

namespace Data.Repositories.Interfaces
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<PagedList<PostOverview>> GetPostsAsync(int brandId,
            PostParams @params);
    }
}
