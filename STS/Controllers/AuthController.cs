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
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest info)
        {
            try
            {
                return Ok(await _service.Register(info));
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
                return Ok(await _service.Login(info));
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
