using System;
using System.Collections.Generic;
using System.Linq;
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

        public WeekScheduleController(IWeekScheduleService weekScheduleService,
            IWeekScheduleDetailService weekScheduleDetailService)
        {
            _weekScheduleService = weekScheduleService;
            _weekScheduleDetailService = weekScheduleDetailService;
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
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{weekScheduleId}/week-schedule-details")]
        public async Task<ActionResult<BrandOverview>> GetWeekScheduleDetails(
            int weekScheduleId, [FromQuery] WeekScheduleDetailParams @params)
        {
            try
            {
                var weekScheduleDetails = await _weekScheduleDetailService
                    .GetWeekScheduleDetails(weekScheduleId, @params);
                Response.AddPaginationHeader(weekScheduleDetails);

                return Ok(weekScheduleDetails);
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
                    Message = ex.Message
                });
            }
        }
    }
}
