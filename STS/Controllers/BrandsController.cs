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
        private readonly IBrandService _service;

        public BrandsController(IBrandService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandOverview>>> GetBrands(
            [FromQuery] BrandParams @params)
        {
            var result = await _service.GetBrands(@params);

            Response.AddPaginationHeader(result.CurrentPage,
                result.PageSize, result.TotalCount, result.TotalPages);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<BrandOverview>>> GetBrand(
            int id)
        {
            return Ok(await _service.GetBrand(id));
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<BrandOverview>>> CreateBrand(
            BrandCreate brand)
        {
            try
            {
                await _service.CreateBrand(brand);
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

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<BrandOverview>>> UpdateBrand(
            int id, BrandUpdate brandUpdate)
        {
            try
            {
                await _service.UpdateBrand(id, brandUpdate);
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
                await _service.DeleteBrand(id);
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
