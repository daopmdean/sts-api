using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;
using STS.Extensions;

namespace STS.Controllers
{
    [Authorize]
    [Route("api/brands")]
    public class BrandsController : ApiBaseController
    {
        private readonly IBrandService _brandService;
        private readonly IStoreService _storeService;
        private readonly IUserService _userService;
        private readonly ISkillService _skillService;
        private readonly IPostService _postService;

        public BrandsController(IBrandService brandService,
            IStoreService storeService, IUserService userService,
            ISkillService skillService, IPostService postService)
        {
            _brandService = brandService;
            _storeService = storeService;
            _userService = userService;
            _skillService = skillService;
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandOverview>>> GetBrands(
            [FromQuery] BrandParams @params)
        {
            try
            {
                var result = await _brandService.GetBrands(@params);
                Response.AddPaginationHeader(result);

                return Ok(result);
            }
            catch (AppException ex)
            {
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAllBrands()
        {
            try
            {
                var result = await _brandService.GetAllBrands();

                return Ok(result);
            }
            catch (AppException ex)
            {
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandOverview>> GetBrand(
            int id)
        {
            try
            {
                return Ok(await _brandService.GetBrand(id));
            }
            catch (AppException ex)
            {
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpGet("stores")]
        public async Task<ActionResult<BrandOverview>> GetStoresOfBrand(
            [FromQuery] StoreParams @params)
        {
            try
            {
                var brandId = int.Parse(User.GetBrandId());
                var stores = await _storeService.GetStores(brandId, @params);
                Response.AddPaginationHeader(stores);
                return Ok(stores);
            }
            catch (AppException ex)
            {
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpGet("stores/all")]
        public async Task<ActionResult<BrandOverview>> GetStoresOfBrand()
        {
            try
            {
                var brandId = int.Parse(User.GetBrandId());
                var stores = await _storeService.GetStores(brandId);
                return Ok(stores);
            }
            catch (AppException ex)
            {
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpGet("users")]
        public async Task<ActionResult<BrandOverview>> GetUsersOfBrand(
            [FromQuery] UserParams @params)
        {
            try
            {
                var brandId = int.Parse(User.GetBrandId());
                var users = await _userService
                    .GetUsersAsync(brandId, @params);

                Response.AddPaginationHeader(users);
                return Ok(users);
            }
            catch (AppException ex)
            {
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpGet("staff")]
        public async Task<ActionResult<BrandOverview>> GetStaffOfBrand(
            [FromQuery] UserParams @params)
        {
            try
            {
                var brandId = int.Parse(User.GetBrandId());
                var staff = await _userService
                    .GetStaffAsync(brandId, @params);

                Response.AddPaginationHeader(staff);
                return Ok(staff);
            }
            catch (AppException ex)
            {
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpGet("store-managers")]
        public async Task<ActionResult<BrandOverview>> GetStoreManagers(
            [FromQuery] UserParams @params)
        {
            try
            {
                var brandId = int.Parse(User.GetBrandId());
                var storeManagers = await _userService
                    .GetStoreManagersAsync(brandId, @params);

                Response.AddPaginationHeader(storeManagers);
                return Ok(storeManagers);
            }
            catch (AppException ex)
            {
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpGet("skills")]
        public async Task<ActionResult> GetSkillsOfBrand(
            [FromQuery] SkillParams @params)
        {
            try
            {
                var brandId = int.Parse(User.GetBrandId());
                var skills = await _skillService.GetSkills(brandId, @params);
                Response.AddPaginationHeader(skills);
                return Ok(skills);
            }
            catch (AppException ex)
            {
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpGet("skills/all")]
        public async Task<ActionResult> GetAllSkillsOfBrand()
        {
            try
            {
                var brandId = int.Parse(User.GetBrandId());
                var skills = await _skillService.GetSkills(brandId);
                return Ok(skills);
            }
            catch (AppException ex)
            {
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpGet("posts")]
        public async Task<ActionResult> GetPostsOfBrand(
            [FromQuery] PostParams @params)
        {
            try
            {
                var brandId = int.Parse(User.GetBrandId());
                var posts = await _postService.GetPosts(brandId, @params);
                Response.AddPaginationHeader(posts);
                return Ok(posts);
            }
            catch (AppException ex)
            {
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<System.Collections.Generic.IEnumerable<BrandOverview>>> CreateBrand(
            BrandCreate brand)
        {
            try
            {
                return Ok(await _brandService.CreateBrand(brand));
            }
            catch (AppException ex)
            {
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<System.Collections.Generic.IEnumerable<BrandOverview>>> UpdateBrand(
            int id, BrandUpdate brandUpdate)
        {
            try
            {
                await _brandService.UpdateBrand(id, brandUpdate);
            }
            catch (AppException ex)
            {
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<System.Collections.Generic.IEnumerable<BrandOverview>>> DeleteBrand(
            int id)
        {
            try
            {
                await _brandService.DeleteBrand(id);
            }
            catch (AppException ex)
            {
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }

            return NoContent();
        }
    }
}
