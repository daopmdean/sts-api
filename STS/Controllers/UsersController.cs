using System;
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
        private readonly IAttendanceService _attendanceService;

        public UsersController(IUserService userService,
            IStaffSkillService staffSkillService,
            IShiftRegisterService shiftRegisterService,
            IShiftAssignmentService shiftAssignmentService,
            IAttendanceService attendanceService)
        {
            _userService = userService;
            _staffSkillService = staffSkillService;
            _shiftRegisterService = shiftRegisterService;
            _shiftAssignmentService = shiftAssignmentService;
            _attendanceService = attendanceService;
        }

        [HttpGet("profile")]
        public async Task<ActionResult> GetUser()
        {
            try
            {
                var username = User.GetUsername();
                var user = await _userService.GetUserAsync(username);

                return Ok(user);
            }
            catch (AppException ex)
            {
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
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
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpGet("shift-registers")]
        public async Task<ActionResult> GetShiftRegistersOfUser(
            [FromQuery] DateTimeParams @params)
        {
            try
            {
                var username = User.GetUsername();
                var shiftRegisters = await _shiftRegisterService
                    .GetShiftRegisters(username, @params);

                return Ok(shiftRegisters);
            }
            catch (AppException ex)
            {
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpGet("shift-assignments")]
        public async Task<ActionResult> GetShiftAssignmentsOfUser(
            [FromQuery] DateTimeParams @params)
        {
            try
            {
                var username = User.GetUsername();
                var shiftAssignments = await _shiftAssignmentService
                    .GetShiftAssignmentOverviews(username, @params);

                return Ok(shiftAssignments);
            }
            catch (AppException ex)
            {
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpGet("attendances")]
        public async Task<ActionResult> GetAttendancesOfUser(
            [FromQuery] DateTimeParams @params)
        {
            try
            {
                var username = User.GetUsername();
                var attendances = await _attendanceService
                    .GetAttendancesAsync(username, @params);

                return Ok(attendances);
            }
            catch (AppException ex)
            {
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpGet("work-report")]
        public async Task<ActionResult> GetWorkReport(
            [FromQuery] DateTimeParams @params)
        {
            try
            {
                var username = User.GetUsername();
                var workHours = await _userService
                    .GetWorkHoursResponse(username, @params);

                return Ok(workHours);
            }
            catch (AppException ex)
            {
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
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
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
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
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }

            return NoContent();
        }
    }
}
