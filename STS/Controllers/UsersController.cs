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
    [Route("api/users")]
    public class UsersController : ApiBaseController
    {
        private readonly IUserService _userService;
        private readonly IStaffSkillService _staffSkillService;
        private readonly IShiftRegisterService _shiftRegisterService;
        private readonly IShiftAssignmentService _shiftAssignmentService;
        private readonly IShiftAttendanceService _shiftAttendanceService;

        public UsersController(IUserService userService,
            IStaffSkillService staffSkillService,
            IShiftRegisterService shiftRegisterService,
            IShiftAssignmentService shiftAssignmentService,
            IShiftAttendanceService shiftAttendanceService)
        {
            _userService = userService;
            _staffSkillService = staffSkillService;
            _shiftRegisterService = shiftRegisterService;
            _shiftAssignmentService = shiftAssignmentService;
            _shiftAttendanceService = shiftAttendanceService;
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

        [HttpGet("profile")]
        public async Task<ActionResult> GetUser()
        {
            var user = await _userService.GetUserAsync(User.GetUsername());

            if (user != null)
                return Ok(user);

            return BadRequest(new ErrorResponse
            {
                StatusCode = 400,
                Message = "Username not found"
            });
        }

        [HttpGet("skills")]
        public async Task<ActionResult> GetSkillsOfUser(
            [FromQuery] StaffSkillParams @params)
        {
            try
            {
                var username = User.GetUsername();

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

        [HttpGet("shift-registers")]
        public async Task<ActionResult> GetShiftRegistersOfUser(
            [FromQuery] ShiftRegisterParams @params)
        {
            try
            {
                var username = User.GetUsername();
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

        [HttpGet("shift-assignments")]
        public async Task<ActionResult> GetShiftAssignmentsOfUser(
            [FromQuery] ShiftAssignmentParams @params)
        {
            try
            {
                var username = User.GetUsername();
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

        [HttpGet("shift-attendances")]
        public async Task<ActionResult> GetShiftAttendancesOfUser(
            [FromQuery] ShiftAttendanceParams @params)
        {
            try
            {
                var username = User.GetUsername();
                var shiftAttendances = await _shiftAttendanceService
                    .GetShiftAttendences(username, @params);
                Response.AddPaginationHeader(shiftAttendances);

                return Ok(shiftAttendances);
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


        [HttpPut]
        public async Task<ActionResult> UpdateUser(UserUpdate updateInfo)
        {
            try
            {
                var loggedInUser = await _userService
                    .GetUserAsync(User.GetUsername());

                await _userService
                    .UpdateUserAsync(loggedInUser.Username, updateInfo);
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

        [HttpPut("password")]
        public async Task<ActionResult> UpdatePassword(
            PasswordUpdate update)
        {
            try
            {
                var loggedInUser = await _userService
                    .GetUserAsync(User.GetUsername());

                await _userService
                    .UpdatePasswordAsync(loggedInUser.Username, update);
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
