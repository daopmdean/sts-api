using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace STS.Controllers
{
    [Route("api/brands")]
    public class BrandsController : ApiBaseController
    {
        public BrandsController()
        {
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandOverview>>> GetBrands()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<BrandOverview>>> GetBrand(
            int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<BrandOverview>>> CreateBrand()
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<BrandOverview>>> UpdateBrand(
            int id)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<BrandOverview>>> DeleteBrand(
            int id)
        {
            return Ok();
        }
    }
}
