﻿using System.Collections.Generic;
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
            PagedList<UserOverview> result = null;
            switch (@params.Position)
            {
                case "brand-manager":
                    result = await _service.GetBrandManagers(@params);
                    break;
                case "store-manager":
                    break;
                case "staff":
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
            return Ok();
        }

    }
}
