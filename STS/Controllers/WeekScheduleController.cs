using System;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;
using STS.Extensions;

namespace STS.Controllers
{
    [Authorize]
    [Route("api/week-schedules")]
    public class WeekScheduleController : ApiBaseController
    {
        private readonly IWeekScheduleService _weekScheduleService;
        private readonly IWeekScheduleDetailService _weekScheduleDetailService;
        private readonly IStaffScheduleDetailService _staffScheduleDetailService;

        public WeekScheduleController(IWeekScheduleService weekScheduleService,
            IWeekScheduleDetailService weekScheduleDetailService,
            IStaffScheduleDetailService staffScheduleDetailService)
        {
            _weekScheduleService = weekScheduleService;
            _weekScheduleDetailService = weekScheduleDetailService;
            _staffScheduleDetailService = staffScheduleDetailService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetWeekSchedule(
            int id)
        {
            try
            {
                return Ok(await _weekScheduleService.GetWeekScheduleAsync(id));
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

        [HttpGet("{weekScheduleId}/week-schedule-details")]
        public async Task<ActionResult> GetWeekScheduleDetails(
            int weekScheduleId, [FromQuery] WeekScheduleDetailParams @params)
        {
            try
            {
                var weekScheduleDetails = await _weekScheduleDetailService
                    .GetWeekScheduleDetailsAsync(weekScheduleId, @params);
                Response.AddPaginationHeader(weekScheduleDetails);

                return Ok(weekScheduleDetails);
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

        [HttpGet("{weekScheduleId}/staff-schedule-details")]
        public async Task<ActionResult> GetStaffScheduleDetails(
            int weekScheduleId, [FromQuery] StaffScheduleDetailParams @params)
        {
            try
            {
                var staffScheduleDetails = await _staffScheduleDetailService
                    .GetStaffScheduleDetails(weekScheduleId, @params);
                Response.AddPaginationHeader(staffScheduleDetails);

                return Ok(staffScheduleDetails);
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

        [HttpPost]
        public async Task<ActionResult<WeekSchedule>> CreateWeekSchedule(
            WeekScheduleCreate weekSchedule)
        {
            try
            {
                return Ok(await _weekScheduleService.CreateWeekScheduleAsync(weekSchedule));
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
