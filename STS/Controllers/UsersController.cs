using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace STS.Controllers
{
    public class UsersController : ApiBaseController
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        // api/users/brand-managements
        [HttpGet("brand-managements")]
        public async Task<ActionResult> GetBrandManagements()
        {
            return Ok(await _service.GetUserOverviews());
        }

        // api/users/brand-managements/{id}
        [HttpGet("brand-managements/{id}")]
        public async Task<ActionResult> GetBrandManagement(string id)
        {
            return Ok("individual brand management: " + id);
        }

        [HttpGet("store-managements")]
        public async Task<ActionResult> GetStoreManagements()
        {
            return Ok("store management");
        }

        [HttpGet("store-managements/{id}")]
        public async Task<ActionResult> GetStoreManagement(string id)
        {
            return Ok("individual store management: " + id);
        }

        [HttpGet("staff")]
        public async Task<ActionResult> GetStaff()
        {
            return Ok("staff");
        }

        [HttpGet("staff/{id}")]
        public async Task<ActionResult> GetIndividualStaff(string id)
        {
            return Ok("staff: " + id);
        }
    }
}
