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
        private readonly ISkillService _skillService;

        public BrandsController(IBrandService brandService,
            IStoreService storeService, ISkillService skillService)
        {
            _brandService = brandService;
            _storeService = storeService;
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandOverview>>> GetBrands(
            [FromQuery] BrandParams @params)
        {
            var result = await _brandService.GetBrands(@params);

            Response.AddPaginationHeader(result.CurrentPage,
                result.PageSize, result.TotalCount, result.TotalPages);

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
                Response.AddPaginationHeader(stores.CurrentPage,
                    stores.PageSize, stores.TotalCount, stores.TotalPages);
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

        [HttpGet("{brandId}/skills")]
        public async Task<ActionResult> GetSkillsOfBrand(
            int brandId, [FromQuery] SkillParams @params)
        {
            try
            {
                var skills = await _skillService.GetSkills(brandId, @params);
                Response.AddPaginationHeader(skills.CurrentPage,
                    skills.PageSize, skills.TotalCount, skills.TotalPages);
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
