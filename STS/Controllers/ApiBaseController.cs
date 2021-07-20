using System;
using Data.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;

namespace STS.Controllers
{
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected ActionResult BadRequestResponse(AppException ex)
        {
            return BadRequest(new ErrorResponse
            {
                StatusCode = ex.StatusCode,
                Message = ex.Message,
                StackTrace = ex.StackTrace
            });
        }

        protected ActionResult InternalErrorResponse(Exception ex)
        {
            return StatusCode(500, new ErrorResponse
            {
                StatusCode = (int)Service.Enums.StatusCode.InternalError,
                Message = ex.Message,
                StackTrace = ex.StackTrace
            });
        }

        protected ActionResult UnauthorizedResponse(AppException ex)
        {
            return Unauthorized(new ErrorResponse
            {
                StatusCode = ex.StatusCode,
                Message = ex.Message,
                StackTrace = ex.StackTrace
            });
        }
    }
}
