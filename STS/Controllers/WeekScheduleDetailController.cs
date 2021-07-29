using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;

namespace STS.Controllers
{
    [Authorize]
    [Route("api/week-schedule-details")]
    public class WeekScheduleDetailController : ApiBaseController
    {
        private readonly IWeekScheduleDetailService _service;

        public WeekScheduleDetailController(IWeekScheduleDetailService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<WeekScheduleDetail>> CreateWeekScheduleDetail(
            IEnumerable<WeekScheduleDetailCreate> weekScheduleDetails)
        {
            try
            {
                return Ok(await _service
                    .CreateWeekScheduleDetailAsync(weekScheduleDetails));
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

        [HttpGet("{id}")]
        public async Task<ActionResult> GetWeekScheduleDetail(
            int id)
        {
            try
            {
                return Ok(await _service.GetWeekScheduleDetail(id));
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

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateWeekScheduleDetail(
            int id, WeekScheduleDetailUpdate update)
        {
            try
            {
                await _service.UpdateWeekScheduleDetailAsync(id, update);
            }
            catch (AppException ex)
            {
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWeekScheduleDetail(
            int id)
        {
            try
            {
                await _service.DeleteWeekScheduleDetailAsync(id);
            }
            catch (AppException ex)
            {
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }

            return NoContent();
        }
    }
}
