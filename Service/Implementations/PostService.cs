using System;
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
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepo;
        private readonly IBrandRepository _brandRepo;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepo,
            IBrandRepository brandRepo, IMapper mapper)
        {
            _postRepo = postRepo;
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        public async Task<Post> CreatePost(PostCreate postCreate)
        {
            var brand = await _brandRepo.GetByIdAsync(postCreate.BrandId);

            if (brand == null)
                throw new AppException(400,
                    "Conflicted with the FOREIGN KEY constraint, brandId does not exist");

            var post = _mapper.Map<Post>(postCreate);
            await _postRepo.CreateAsync(post);

            if (await _postRepo.SaveChangesAsync())
                return post;

            throw new AppException(400, "Can not create post");
        }

        public async Task DeletePost(int id)
        {
            var post = await _postRepo.GetByIdAsync(id);

            if (post == null)
                throw new AppException(400, "Post not found");

            _postRepo.Delete(post);

            if (await _postRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not delete post");
        }

        public async Task<Post> GetPost(int id)
        {
            var post = await _postRepo.GetByIdAsync(id);

            if (post == null)
                throw new AppException(400, "Post not found or has been deleted");

            return post;
        }

        public Task<PagedList<PostOverview>> GetPosts(PostParams @params)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<PostOverview>> GetPosts(int brandId,
            PostParams @params)
        {
            return await _postRepo.GetPostsAsync(brandId, @params);
        }

        public async Task UpdatePost(int id, PostUpdate postUpdate)
        {
            var post = await _postRepo.GetByIdAsync(id);

            if (post == null)
                throw new AppException(400, "Post not found");

            _mapper.Map(postUpdate, post);

            _postRepo.Update(post);

            if (await _postRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not update post");
        }
    }
}
