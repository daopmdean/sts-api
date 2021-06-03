using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace STS.Controllers
{
    [Route("api/stores")]
    public class StoresController : ApiBaseController
    {
        public StoresController()
        {
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreOverview>>> GetStores()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<BrandOverview>>> GetStore(
            int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<BrandOverview>>> CreateStore()
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<BrandOverview>>> UpdateStore(
            int id)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<BrandOverview>>> DeleteStore(
            int id)
        {
            return Ok();
        }
    }
}
