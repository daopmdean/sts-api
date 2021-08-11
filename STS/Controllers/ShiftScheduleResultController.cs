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
                await _scheduleResultService
                    .CreateShiftScheduleResult(create);
                return Ok("success");
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
