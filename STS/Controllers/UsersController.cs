using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models.Responses;
using Data.Pagings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using STS.Extensions;

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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserOverview>>> GetUsers(
            [FromQuery] UserParams @params)
        {
            PagedList<UserOverview> result;

            switch (@params.Position)
            {
                case "brand-manager":
                    result = await _service.GetBrandManagers(@params);
                    break;
                case "store-manager":
                    result = await _service.GetStoreManagers(@params);
                    break;
                case "staff":
                    result = await _service.GetStaff(@params);
                    break;
                default:
                    return BadRequest();
            }
            Response.AddPaginationHeader(result.CurrentPage,
                result.PageSize, result.TotalCount, result.TotalPages);

            return Ok(result);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult> GetUser(string username)
        {
            var user = await _service.GetUser(username);
            if (user != null)
                return Ok(user);

            return BadRequest(new ErrorResponse
            {
                StatusCode = 400,
                Message = "Username not found"
            });
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(string username)
        {
            var user = await _service.GetUser(username);
            if (user != null)
                return Ok(user);

            return BadRequest(new ErrorResponse
            {
                StatusCode = 400,
                Message = "Username not found"
            });
        }

        [HttpPut("{username}")]
        public async Task<ActionResult> UpdateUser(string username)
        {
            var user = await _service.GetUser(username);
            if (user != null)
                return Ok(user);

            return BadRequest(new ErrorResponse
            {
                StatusCode = 400,
                Message = "Username not found"
            });
        }

        [HttpDelete("{username}")]
        public async Task<ActionResult> DeleteUser(string username)
        {
            var user = await _service.GetUser(username);
            if (user != null)
                return Ok(user);

            return BadRequest(new ErrorResponse
            {
                StatusCode = 400,
                Message = "Username not found"
            });
        }
    }
}
