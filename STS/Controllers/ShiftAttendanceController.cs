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
    [Route("api/shift-attendances")]
    public class ShiftAttendanceController : ApiBaseController
    {
        private readonly IShiftAttendanceService _service;

        public ShiftAttendanceController(
            IShiftAttendanceService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<ShiftAttendance>> CreateShiftAttendance(
            ShiftAttendanceCreate create)
        {
            try
            {
                return Ok(await _service.CreateShiftAttendance(create));
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

        [HttpGet("{id}")]
        public async Task<ActionResult> GetShiftAttendance(
            int id)
        {
            try
            {
                return Ok(await _service.GetShiftAttendance(id));
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

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateShiftAttendance(
            int id, ShiftAttendanceUpdate update)
        {
            try
            {
                await _service.UpdateShiftAttendance(id, update);
            }
            catch (AppException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message
                });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteShiftAttendance(
            int id)
        {
            try
            {
                await _service.DeleteShiftAttendance(id);
            }
            catch (AppException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message
                });
            }

            return NoContent();
        }
    }
}
