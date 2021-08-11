using System;
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
    [Route("api/staff-skill")]
    public class StaffSkillController : ApiBaseController
    {
        private readonly IStaffSkillService _service;

        public StaffSkillController(IStaffSkillService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<StoreStaff>> CreateStaffSkill(
            StaffSkillCreate create)
        {
            try
            {
                return Ok(await _service.CreateStaffSkill(create));
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

        [HttpGet("{skillId}/{username}")]
        public async Task<ActionResult> GetStaffSkill(
            int skillId, string username)
        {
            try
            {
                return Ok(await _service
                    .GetStaffSkillAsync(skillId, username));
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

        [HttpPut("{staffId}/{username}")]
        public async Task<ActionResult> UpdateStaffSkill(
            int staffId, string username, StaffSkillUpdate update)
        {
            try
            {
                await _service
                    .UpdateStaffSkill(staffId, username, update);
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

        [HttpDelete("{skillId}/{username}")]
        public async Task<ActionResult> DeleteSkill(
            int skillId, string username)
        {
            try
            {
                await _service.DeleteStaffSkill(skillId, username);
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
