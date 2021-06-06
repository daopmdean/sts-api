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

        public BrandsController(IBrandService brandService,
            IStoreService storeService)
        {
            _brandService = brandService;
            _storeService = storeService;
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
                return Ok(await _storeService.GetStores(brandId, @params));
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
