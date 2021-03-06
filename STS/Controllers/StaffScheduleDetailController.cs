using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;

namespace STS.Controllers
{
    [Authorize]
    [Route("api/staff-schedule-details")]
    public class StaffScheduleDetailController : ApiBaseController
    {
        private readonly IStaffScheduleDetailService _service;

        public StaffScheduleDetailController(IStaffScheduleDetailService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<StaffScheduleDetail>> CreateStaffScheduleDetail(
            IEnumerable<StaffScheduleDetailCreate> staffScheduleDetails)
        {
            try
            {
                return Ok(await _service
                    .CreateStaffScheduleDetailAsync(staffScheduleDetails));
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
        public async Task<ActionResult> GetStaffScheduleDetail(
            int id)
        {
            try
            {
                return Ok(await _service.GetStaffScheduleDetail(id));
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
        public async Task<ActionResult> UpdateStaffScheduleDetail(
            int id, StaffScheduleDetailUpdate update)
        {
            try
            {
                await _service.UpdateStaffScheduleDetailAsync(id, update);
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
        public async Task<ActionResult> DeleteStaffScheduleDetail(
            int id)
        {
            try
            {
                await _service.DeleteStaffScheduleDetailAsync(id);
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
