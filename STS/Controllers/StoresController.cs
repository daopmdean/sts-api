using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;
using STS.Extensions;

namespace STS.Controllers
{
    [Authorize]
    [Route("api/stores")]
    public class StoresController : ApiBaseController
    {
        private readonly IStoreService _storeService;
        private readonly IWeekScheduleService _weekService;
        private readonly IStoreStaffService _storeStaffService;

        public StoresController(IStoreService storeService,
            IWeekScheduleService weekService,
            IStoreStaffService storeStaffService)
        {
            _storeService = storeService;
            _weekService = weekService;
            _storeStaffService = storeStaffService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreOverview>>> GetStores(
            [FromQuery] StoreParams @params)
        {
            var result = await _storeService.GetStores(@params);

            Response.AddPaginationHeader(result.CurrentPage,
                result.PageSize, result.TotalCount, result.TotalPages);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetStore(
            int id)
        {
            try
            {
                return Ok(await _storeService.GetStore(id));
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

        [HttpGet("{storeId}/week-schedules")]
        public async Task<ActionResult<BrandOverview>> GetStoresOfBrand(
            int storeId, [FromQuery] WeekScheduleParams @params)
        {
            try
            {
                var weekSchedules = await _weekService
                    .GetWeekSchedulesAsync(storeId, @params);
                Response.AddPaginationHeader(weekSchedules);

                return Ok(weekSchedules);
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

        [HttpGet("{storeId}/staff")]
        public async Task<ActionResult<BrandOverview>> GetStaffOfStore(
            int storeId, [FromQuery] StoreStaffParams @params)
        {
            try
            {
                var staff = await _storeStaffService
                    .GetStaffFromStoreAsync(storeId, @params);
                Response.AddPaginationHeader(staff);

                return Ok(staff);
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

        [HttpPost]
        public async Task<ActionResult<Store>> CreateStore(
            StoreCreate store)
        {
            try
            {
                return Ok(await _storeService.CreateStore(store));
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
        public async Task<ActionResult> UpdateStore(
            int id, StoreUpdate storeUpdate)
        {
            try
            {
                await _storeService.UpdateStore(id, storeUpdate);
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
        public async Task<ActionResult<IEnumerable<BrandOverview>>> DeleteStore(
            int id)
        {
            try
            {
                await _storeService.DeleteStore(id);
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
