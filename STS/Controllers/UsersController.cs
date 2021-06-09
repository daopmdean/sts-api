﻿using System.Collections.Generic;
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
    [Route("api/users")]
    public class UsersController : ApiBaseController
    {
        private readonly IUserService _userService;
        private readonly IStaffSkillService _staffSkillService;
        private readonly IShiftRegisterService _shiftRegisterService;
        private readonly IShiftAssignmentService _shiftAssignmentService;

        public UsersController(IUserService userService,
            IStaffSkillService staffSkillService,
            IShiftRegisterService shiftRegisterService,
            IShiftAssignmentService shiftAssignmentService)
        {
            _userService = userService;
            _staffSkillService = staffSkillService;
            _shiftRegisterService = shiftRegisterService;
            _shiftAssignmentService = shiftAssignmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserOverview>>> GetUsers(
            [FromQuery] UserParams @params)
        {
            PagedList<UserOverview> result;
            result = await _userService.GetUsersAsync(@params);

            Response.AddPaginationHeader(result);

            return Ok(result);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult> GetUser(string username)
        {
            var user = await _userService.GetUserAsync(username);

            if (user != null)
                return Ok(user);

            return BadRequest(new ErrorResponse
            {
                StatusCode = 400,
                Message = "Username not found"
            });
        }

        [HttpGet("{username}/skills")]
        public async Task<ActionResult> GetSkillsOfUser(string username,
            [FromQuery] StaffSkillParams @params)
        {
            try
            {
                var skills = await _staffSkillService
                    .GetSkillsFromStaffAsync(username, @params);
                Response.AddPaginationHeader(skills);

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

        [HttpGet("{username}/shift-registers")]
        public async Task<ActionResult> GetShiftRegistersOfUser(
            string username, [FromQuery] ShiftRegisterParams @params)
        {
            try
            {
                var shiftRegisters = await _shiftRegisterService
                    .GetShiftRegisters(username, @params);
                Response.AddPaginationHeader(shiftRegisters);

                return Ok(shiftRegisters);
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

        [HttpGet("{username}/shift-assignments")]
        public async Task<ActionResult> GetShiftAssignmentsOfUser(
            string username, [FromQuery] ShiftAssignmentParams @params)
        {
            try
            {
                var shiftAssignments = await _shiftAssignmentService
                    .GetShiftAssignments(username, @params);
                Response.AddPaginationHeader(shiftAssignments);

                return Ok(shiftAssignments);
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


        [HttpPut("{username}")]
        public async Task<ActionResult> UpdateUser(string username,
            UserUpdate updateInfo)
        {
            var loggedInUser = await _userService.GetUserAsync(User.GetUsername());

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
                await _userService.UpdateUserAsync(username, updateInfo);
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
                await _userService.DeleteUserAsync(username);
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
