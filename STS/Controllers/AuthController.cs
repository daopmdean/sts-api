using System;
using System.Threading.Tasks;
using Data.Models.Requests;
using Data.Models.Responses;
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest info)
        {
            try
            {
                return Ok(await _service.Login(info));
            }
            catch (AppException appEx)
            {
                return Unauthorized(new ErrorResponse
                {
                    StatusCode = appEx.StatusCode,
                    Message = appEx.Message
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new ErrorResponse
                {
                    Message = ex.Message
                });
            }
        }
    }
}
