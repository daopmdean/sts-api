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
    [Route("api/shift-assignments")]
    public class ShiftAssignmentController : ApiBaseController
    {
        private readonly IShiftAssignmentService _shiftAssignmentService;

        public ShiftAssignmentController(IShiftAssignmentService service)
        {
            _shiftAssignmentService = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetShiftAssignment(
            int id)
        {
            try
            {
                return Ok(await _shiftAssignmentService.GetShiftAssignment(id));
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

        [HttpPost]
        public async Task<ActionResult> Assignments(
            IEnumerable<ShiftAssignmentCreate> create)
        {
            try
            {
                return Ok(await _shiftAssignmentService
                    .CreateShiftAssignments(create));
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
        public async Task<ActionResult> UpdateShiftAssignment(
            int id, ShiftAssignmentUpdate update)
        {
            try
            {
                await _shiftAssignmentService.UpdateShiftAssignment(id, update);
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
        public async Task<ActionResult> DeleteShiftAssignment(
            int id)
        {
            try
            {
                await _shiftAssignmentService.DeleteShiftAssignment(id);
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
