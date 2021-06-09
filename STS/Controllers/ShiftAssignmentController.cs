using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace STS.Controllers
{
    [Authorize]
    [Route("api/shift-assignments")]
    public class ShiftAssignmentController : ApiBaseController
    {
        private readonly IShiftAssignmentService _service;

        public ShiftAssignmentController(IShiftAssignmentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<ShiftRegister>> CreateShiftAssignment(
            ShiftAssignmentCreate create)
        {
            try
            {
                return Ok(await _service.CreateShiftAssignment(create));
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
        public async Task<ActionResult> GetShiftAssignment(
            int id)
        {
            try
            {
                return Ok(await _service.GetShiftAssignment(id));
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
        public async Task<ActionResult> UpdateShiftAssignment(
            int id, ShiftAssignmentUpdate update)
        {
            try
            {
                await _service.UpdateShiftAssignment(id, update);
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
                await _service.DeleteShiftAssignment(id);
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
