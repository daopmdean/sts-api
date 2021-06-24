using System;
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
    [Route("api/manager")]
    public class ManagerController : ApiBaseController
    {
        private readonly IManagerService _managerService;
        private readonly IStoreStaffService _storeStaffService;
        private readonly IWeekScheduleService _weekService;
        private readonly IStoreService _storeService;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public ManagerController(
            IManagerService managerService,
            IStoreStaffService storeStaffService,
            IWeekScheduleService weekService,
            IStoreService storeService,
            IUserService userService,
            IAuthService authService)
        {
            _managerService = managerService;
            _storeStaffService = storeStaffService;
            _weekService = weekService;
            _storeService = storeService;
            _userService = userService;
            _authService = authService;
        }

        [HttpGet("stores/{storeId}/week-schedules")]
        public async Task<ActionResult<BrandOverview>> GetStoresOfBrand(
            int storeId, [FromQuery] WeekScheduleParams @params)
        {
            try
            {
                var weekSchedules = await _weekService
                    .GetWeekSchedulesAsync(storeId, @params);
                Response.AddPaginationHeader(weekSchedules);

                return Ok(weekSchedules);
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

        [HttpGet("stores/{storeId}/staff")]
        public async Task<ActionResult<BrandOverview>> GetStaffOfStore(
            int storeId, [FromQuery] StoreStaffParams @params)
        {
            try
            {
                var staff = await _storeStaffService
                    .GetStaffFromStoreAsync(storeId, @params);
                Response.AddPaginationHeader(staff);

                return Ok(staff);
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

        [HttpPost("users/store-manager")]
        public async Task<IActionResult> RegisterStoreManager(RegisterRequest info)
        {
            try
            {
                var brandId = int.Parse(User.GetBrandId());

                return Ok(await _authService
                    .RegisterWithRole(brandId, (int)UserRole.StoreManager, info));
            }
            catch (AppException appEx)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = appEx.StatusCode,
                    Message = appEx.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                });
            }
        }

        [HttpPost("users/staff")]
        public async Task<IActionResult> RegisterStaff(RegisterRequest info)
        {
            try
            {
                var brandId = int.Parse(User.GetBrandId());

                return Ok(await _authService
                    .RegisterWithRole(brandId, (int)UserRole.Staff, info));
            }
            catch (AppException appEx)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = appEx.StatusCode,
                    Message = appEx.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                });
            }
        }

        [HttpPost("assign/store-manager")]
        public async Task<IActionResult> AssignStoreManager(StoreAssign info)
        {
            try
            {
                await _storeStaffService.AssignStoreManager(info);
            }
            catch (AppException appEx)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = appEx.StatusCode,
                    Message = appEx.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                });
            }

            return NoContent();
        }

        [HttpPut("stores/{id}")]
        public async Task<ActionResult> UpdateStore(
            int id, StoreUpdate storeUpdate)
        {
            try
            {
                await _storeService.UpdateStore(id, storeUpdate);
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