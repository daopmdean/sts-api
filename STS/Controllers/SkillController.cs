using System;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;
using STS.Extensions;

namespace STS.Controllers
{
    [Authorize]
    [Route("api/skills")]
    public class SkillController : ApiBaseController
    {
        private readonly ISkillService _service;

        public SkillController(ISkillService service)
        {
            _service = service;
        }

        [Authorize(Policy = "RequiredBrandManager")]
        [HttpPost]
        public async Task<ActionResult<Skill>> CreateSkill(
            SkillCreate skill)
        {
            try
            {
                var brandId = User.GetBrandId();

                if (brandId == null || brandId == "")
                    throw new Exception();

                return Ok(await _service
                    .CreateSkill(int.Parse(brandId), skill));
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
        public async Task<ActionResult> GetSkill(
            int id)
        {
            try
            {
                return Ok(await _service.GetSkill(id));
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
        public async Task<ActionResult> UpdateSkill(
            int id, SkillUpdate skillUpdate)
        {
            try
            {
                await _service.UpdateSkill(id, skillUpdate);
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
        public async Task<ActionResult> DeleteSkill(
            int id)
        {
            try
            {
                await _service.DeleteSkill(id);
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
