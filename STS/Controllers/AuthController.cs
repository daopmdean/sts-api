using System;
using System.Threading.Tasks;
using Data.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;

namespace STS.Controllers
{
    [Route("api/auth")]
    public class AuthController : ApiBaseController
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(
            IAuthService authService,
            IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(BrandManagerCreate info)
        {
            try
            {
                return Ok(await _authService.CreateBrandManager(info));
            }
            catch (AppException appEx)
            {
                return UnauthorizedResponse(appEx);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest info)
        {
            try
            {
                return Ok(await _authService.Login(info));
            }
            catch (AppException ex)
            {
                return UnauthorizedResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpPost("restore")]
        public async Task<IActionResult> RestorePassword(PasswordRestore info)
        {
            try
            {
                await _userService.RestorePasswordAsync(info.Username);
                return Ok();
            }
            catch (AppException ex)
            {
                return UnauthorizedResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }
    }
}
