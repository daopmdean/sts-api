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

        public BrandsController(IBrandService brandService,
            IStoreService storeService, IUserService userService,
            ISkillService skillService)
        {
            _brandService = brandService;
            _storeService = storeService;
            _userService = userService;
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandOverview>>> GetBrands(
            [FromQuery] BrandParams @params)
        {
            var result = await _brandService.GetBrands(@params);

            Response.AddPaginationHeader(result);

            return Ok(result);
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
                return BadRequest(new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{brandId}/stores")]
        public async Task<ActionResult<BrandOverview>> GetStoresOfBrand(
            int brandId, [FromQuery] StoreParams @params)
        {
            try
            {
                var stores = await _storeService.GetStores(brandId, @params);
                Response.AddPaginationHeader(stores);
                return Ok(stores);
            }
            catch (AppException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{brandId}/users")]
        public async Task<ActionResult<BrandOverview>> GetUsersOfBrand(
            int brandId, [FromQuery] UserParams @params)
        {
            try
            {
                var users = await _userService.GetUsersAsync(brandId, @params);

                Response.AddPaginationHeader(users);
                return Ok(users);
            }
            catch (AppException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{brandId}/skills")]
        public async Task<ActionResult> GetSkillsOfBrand(
            int brandId, [FromQuery] SkillParams @params)
        {
            try
            {
                var skills = await _skillService.GetSkills(brandId, @params);
                Response.AddPaginationHeader(skills);
                return Ok(skills);
            }
            catch (AppException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<BrandOverview>>> CreateBrand(
            BrandCreate brand)
        {
            try
            {
                return Ok(await _brandService.CreateBrand(brand));
            }
            catch (AppException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<BrandOverview>>> UpdateBrand(
            int id, BrandUpdate brandUpdate)
        {
            try
            {
                await _brandService.UpdateBrand(id, brandUpdate);
            }
            catch (AppException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message
                });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<BrandOverview>>> DeleteBrand(
            int id)
        {
            try
            {
                await _brandService.DeleteBrand(id);
            }
            catch (AppException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message
                });
            }

            return NoContent();
        }
    }
}
