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

namespace STS.Controllers
{
    [Authorize]
    [Route("api/store-schedule-details")]
    public class StoreScheduleDetailController : ApiBaseController
    {
        private readonly IStoreScheduleDetailService _service;

        public StoreScheduleDetailController(IStoreScheduleDetailService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<StoreScheduleDetail>> CreateStoreScheduleDetail(
            IEnumerable<StoreScheduleDetailCreate> create)
        {
            try
            {
                return Ok(await _service
                    .CreateStoreScheduleDetailAsync(create));
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
        public async Task<ActionResult> GetStoreScheduleDetail(
            int id)
        {
            try
            {
                return Ok(await _service.GetStoreScheduleDetail(id));
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
        public async Task<ActionResult> UpdateStoreScheduleDetail(
            int id, StoreScheduleDetailUpdate update)
        {
            try
            {
                await _service.UpdateStoreScheduleDetailAsync(id, update);
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
        public async Task<ActionResult> DeleteStoreScheduleDetail(
            int id)
        {
            try
            {
                await _service.DeleteStoreScheduleDetailAsync(id);
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
