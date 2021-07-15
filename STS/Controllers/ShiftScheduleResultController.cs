using Data.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace STS.Controllers
{
    [Route("api/shift-schedule")]
    public class ShiftScheduleResultController : ApiBaseController
    {
        private readonly IShiftScheduleResultService _scheduleResultService;
        public ShiftScheduleResultController(
            IShiftScheduleResultService scheduleResultService)
        {
            _scheduleResultService = scheduleResultService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateShiftSchedule(
            ScheduleResponse create)
        {
            try
            {
                await _scheduleResultService.CreateShiftScheduleResult(create);
                return Ok("success");
            }
            catch (AppException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = (int)Service.Enums.StatusCode.InternalError,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                });
            }
        }
    }
}
