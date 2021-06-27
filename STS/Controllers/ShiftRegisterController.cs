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
    [Route("api/shift-registers")]
    public class ShiftRegisterController : ApiBaseController
    {
        private readonly IShiftRegisterService _service;

        public ShiftRegisterController(IShiftRegisterService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<ShiftRegister>> CreateShiftRegister(
            ShiftRegistersCreate create)
        {
            try
            {
                return CreatedAtRoute("", await _service
                    .CreateShiftRegister(create));
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
        public async Task<ActionResult> GetShiftRegister(
            int id)
        {
            try
            {
                return Ok(await _service.GetShiftRegister(id));
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
        public async Task<ActionResult> UpdateShiftRegister(
            int id, ShiftRegisterUpdate update)
        {
            try
            {
                await _service.UpdateShiftRegister(id, update);
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
        public async Task<ActionResult> DeleteShiftRegister(
            int id)
        {
            try
            {
                await _service.DeleteShiftRegister(id);
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
