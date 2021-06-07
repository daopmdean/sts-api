using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
    [Route("api/users")]
    public class UsersController : ApiBaseController
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserOverview>>> GetUsers(
            [FromQuery] UserParams @params)
        {
            PagedList<UserOverview> result;
            result = await _service.GetUsersAsync(@params);

            Response.AddPaginationHeader(result);

            return Ok(result);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult> GetUser(string username)
        {
            var user = await _service.GetUserAsync(username);

            if (user != null)
                return Ok(user);

            return BadRequest(new ErrorResponse
            {
                StatusCode = 400,
                Message = "Username not found"
            });
        }


        [HttpPut("{username}")]
        public async Task<ActionResult> UpdateUser(string username,
            UserUpdate updateInfo)
        {
            var loggedInUser = await _service.GetUserAsync(User.GetUsername());

            if (loggedInUser.Username != username)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = 400,
                    Message = "You can not edit other user"
                });
            }

            try
            {
                await _service.UpdateUserAsync(username, updateInfo);
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

        [HttpDelete("{username}")]
        public async Task<ActionResult> DeleteUser(string username)
        {
            try
            {
                await _service.DeleteUserAsync(username);
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
