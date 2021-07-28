using System;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;

namespace STS.Controllers
{
    [Route("api/attendances")]
    public class AttendanceController : ApiBaseController
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(
            IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ShiftAttendance>> CreateAttendance(
            AttendanceCreate create)
        {
            try
            {
                return Ok(await _attendanceService.CreateAttendanceAsync(create));
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
        public async Task<ActionResult> GetAttendance(
            int id)
        {
            try
            {
                return Ok(await _attendanceService.GetAttendanceAsync(id));
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
        public async Task<ActionResult> UpdateAttendance(
            int id, AttendanceUpdate update)
        {
            try
            {
                await _attendanceService.UpdateAttendanceAsync(id, update);
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
        public async Task<ActionResult> DeleteAttendance(
            int id)
        {
            try
            {
                await _attendanceService.DeleteAttendanceAsync(id);
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
