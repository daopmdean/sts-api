using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;

namespace STS.Controllers
{
    [Authorize]
    [Route("api/store-staff")]
    public class StoreStaffController : ApiBaseController
    {
        private readonly IStoreStaffService _service;
        private readonly IStoreStaffRepository _repo;

        public StoreStaffController(
            IStoreStaffService service,
            IStoreStaffRepository repo)
        {
            _service = service;
            _repo = repo;
        }

        [HttpPost]
        public async Task<ActionResult<StoreStaff>> CreateStoreStaff(
            StoreStaffCreate create)
        {
            try
            {
                return Ok(await _service.CreateStoreStaff(create));
            }
            catch (AppException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = (int)Service.Enums.StatusCode.InternalError,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                });
            }
        }

        [HttpGet("{storeId}/{username}")]
        public async Task<ActionResult> GetStoreStaff(
            int storeId, string username)
        {
            try
            {
                return Ok(await _service
                    .GetStoreStaffAsync(storeId, username));
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

        [HttpPut("{storeId}/{username}")]
        public async Task<ActionResult> UpdateStoreStaff(
            int storeId, string username, StoreStaffUpdate update)
        {
            try
            {
                await _service
                    .UpdateStoreStaff(storeId, username, update);
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

        [HttpDelete("{storeId}/{username}")]
        public async Task<ActionResult> DeleteSkill(
            int storeId, string username)
        {
            try
            {
                await _service.DeleteStoreStaff(storeId, username);
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
