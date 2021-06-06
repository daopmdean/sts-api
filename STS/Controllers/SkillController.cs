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
    [Route("api/skills")]
    public class SkillController : ApiBaseController
    {
        private readonly ISkillService _service;

        public SkillController(ISkillService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Skill>> CreateSkill(
            SkillCreate skill)
        {
            try
            {
                return Ok(await _service.CreateSkill(skill));
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
        public async Task<ActionResult> GetSkill(
            int id)
        {
            try
            {
                return Ok(await _service.GetSkill(id));
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
        public async Task<ActionResult> UpdateSkill(
            int id, SkillUpdate skillUpdate)
        {
            try
            {
                await _service.UpdateSkill(id, skillUpdate);
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
        public async Task<ActionResult> DeleteSkill(
            int id)
        {
            try
            {
                await _service.DeleteSkill(id);
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
