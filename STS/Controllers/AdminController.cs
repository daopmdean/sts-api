using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Enums;
using Service.Exceptions;
using Service.Interfaces;
using STS.Extensions;

namespace STS.Controllers
{
    [Authorize(Policy = "RequiredManagers")]
    [Route("api/admin")]
    public class AdminController : ApiBaseController
    {
        private readonly IAdminService _adminService;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IStaffSkillService _staffSkillService;
        private readonly IShiftRegisterService _shiftRegisterService;
        private readonly IShiftAssignmentService _shiftAssignmentService;
        private readonly IShiftAttendanceService _shiftAttendanceService;
        private readonly IStoreService _storeService;
        private readonly ISkillService _skillService;
        private readonly IPostService _postService;

        public AdminController(
            IAdminService adminService,
            IAuthService authService,
            IUserService userService,
            IStaffSkillService staffSkillService,
            IShiftRegisterService shiftRegisterService,
            IShiftAssignmentService shiftAssignmentService,
            IShiftAttendanceService shiftAttendanceService,
            IStoreService storeService,
            ISkillService skillService,
            IPostService postService)
        {
            _adminService = adminService;
            _authService = authService;
            _userService = userService;
            _staffSkillService = staffSkillService;
            _shiftRegisterService = shiftRegisterService;
            _shiftAssignmentService = shiftAssignmentService;
            _shiftAttendanceService = shiftAttendanceService;
            _storeService = storeService;
            _skillService = skillService;
            _postService = postService;
        }

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UserOverview>>> GetUsers(
            [FromQuery] UserParams @params)
        {
            try
            {
                var result = await _userService.GetUsersAsync(@params);
                Response.AddPaginationHeader(result);

                return Ok(result);
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

        [HttpGet("users/{username}")]
        public async Task<ActionResult> GetUser(string username)
        {
            try
            {
                var user = await _userService
                    .GetUserGeneralAsync(username);

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

        [HttpGet("users/{username}/skills")]
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
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpGet("users/{username}/shift-registers")]
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
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpGet("users/{username}/shift-assignments")]
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
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpGet("users/{username}/shift-attendances")]
        public async Task<ActionResult> GetShiftAttendancesOfUser(
            string username, [FromQuery] ShiftAttendanceParams @params)
        {
            try
            {
                var shiftAttendances = await _shiftAttendanceService
                    .GetShiftAttendencesAsync(username, @params);
                Response.AddPaginationHeader(shiftAttendances);

                return Ok(shiftAttendances);
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

        [HttpDelete("users/{username}")]
        public async Task<ActionResult> DeleteUser(string username)
        {
            try
            {
                await _userService.DeleteUserAsync(username);
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

        [HttpGet("{brandId}/stores")]
        public async Task<ActionResult<BrandOverview>> GetStoresOfBrand(
            int brandId, [FromQuery] StoreParams @params)
        {
            try
            {
                var stores = await _storeService.GetStores(brandId, @params);
                Response.AddPaginationHeader(stores);
                return Ok(stores);
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

        [HttpGet("{brandId}/users")]
        public async Task<ActionResult<BrandOverview>> GetUsersOfBrand(
            int brandId, [FromQuery] UserParams @params)
        {
            try
            {
                var users = await _userService.GetUsersAsync(brandId, @params);

                Response.AddPaginationHeader(users);
                return Ok(users);
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

        [HttpGet("{brandId}/skills")]
        public async Task<ActionResult> GetSkillsOfBrand(
            int brandId, [FromQuery] SkillParams @params)
        {
            try
            {
                var skills = await _skillService.GetSkills(brandId, @params);
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

        [HttpGet("{brandId}/posts")]
        public async Task<ActionResult> GetPostsOfBrand(
            int brandId, [FromQuery] PostParams @params)
        {
            try
            {
                var posts = await _postService.GetPosts(brandId, @params);
                Response.AddPaginationHeader(posts);
                return Ok(posts);
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

        [HttpPost("assign/brand")]
        public async Task<ActionResult> AssignBrand(BrandAssign brandAssign)
        {
            try
            {
                await _adminService.AssignBrand(brandAssign);
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

        [HttpPost("users/admin")]
        public async Task<ActionResult> NewAdmin(RegisterRequest info)
        {
            try
            {
                var result = await _authService
                    .RegisterWithRole(0, (int)UserRole.Admin, info);

                return Ok(result);
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
    }
}
