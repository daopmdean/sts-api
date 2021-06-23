using System;
using System.Threading.Tasks;
using Data.Models.Requests;
using Data.Models.Responses;
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
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public ManagerController(
            IManagerService managerService,
            IUserService userService,
            IAuthService authService)
        {
            _managerService = managerService;
            _userService = userService;
            _authService = authService;
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

        [HttpPost("assign/store")]
        public async Task<IActionResult> AssignStoreManager(StoreAssign info)
        {
            try
            {
                await _managerService.AssignStoreManager(info);
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
    }
}