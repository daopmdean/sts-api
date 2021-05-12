using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;
using Service.Models.Requests;
using Service.Models.Responses;

namespace STS.Controllers
{
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
                return BadRequest(new ErrorResponses
                {
                    StatusCode = appEx.StatusCode,
                    Message = appEx.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponses
                {
                    Message = ex.Message
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
                return Unauthorized(new ErrorResponses
                {
                    StatusCode = appEx.StatusCode,
                    Message = appEx.Message
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new ErrorResponses
                {
                    Message = ex.Message
                });
            }
        }
    }
}
