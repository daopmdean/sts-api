using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;

namespace Service.Interfaces
{
    public interface IPostService
    {
        Task<PagedList<PostOverview>> GetPosts(PostParams @params);
        Task<PagedList<PostOverview>> GetPosts(int brandId,
            PostParams @params);
        Task<Post> GetPost(int id);
        Task<Post> CreatePost(PostCreate postCreate);
        Task UpdatePost(int id, PostUpdate postUpdate);
        Task DeletePost(int id);
    }
}
