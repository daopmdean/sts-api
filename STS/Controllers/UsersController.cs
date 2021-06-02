using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace STS.Controllers
{
    [Route("api/users")]
    public class UsersController : ApiBaseController
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [Authorize(Policy = "RequiredStaff")]
        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            return Ok(await _service.GetUserOverviews());
        }

        [HttpGet("{username}")]
        public async Task<ActionResult> GetUser(string username)
        {
            return Ok(await _service.GetUserOverviews());
        }

    }
}
