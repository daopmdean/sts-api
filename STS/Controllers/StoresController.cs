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
        private readonly IShiftAssignmentService _shiftAssignmentService;
        private readonly IShiftAttendanceService _shiftAttendanceService;

        public StoresController(IStoreService storeService,
            IWeekScheduleService weekService,
            IStoreStaffService storeStaffService,
            IShiftAssignmentService shiftAssignmentService,
            IShiftAttendanceService shiftAttendanceService)
        {
            _storeService = storeService;
            _weekService = weekService;
            _storeStaffService = storeStaffService;
            _shiftAssignmentService = shiftAssignmentService;
            _shiftAttendanceService = shiftAttendanceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreOverview>>> GetStores(
            [FromQuery] StoreParams @params)
        {
            try
            {
                var result = await _storeService.GetStores(@params);
                Response.AddPaginationHeader(result);

                return Ok(result);
            }
            catch (AppException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStore(
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
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
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

        [HttpGet("week-schedules")]
        public async Task<ActionResult<BrandOverview>> GetStoresOfBrand(
            [FromQuery] WeekScheduleParams @params)
        {
            try
            {
                int storeId = int.Parse(User.GetStoreId());
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
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
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

        [HttpGet("staff")]
        public async Task<ActionResult<IEnumerable<StoreStaffOverview>>> GetStaffOfStore(
            [FromQuery] StoreStaffParams @params)
        {
            try
            {
                int storeId = int.Parse(User.GetStoreId());
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
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
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

        [HttpGet("staff/all")]
        public async Task<ActionResult<IEnumerable<StoreStaffOverview>>> GetStaffOfStore()
        {
            try
            {
                int storeId = int.Parse(User.GetStoreId());
                var staff = await _storeStaffService
                    .GetStaffFromStoreAsync(storeId);
                return Ok(staff);
            }
            catch (AppException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
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

        [HttpGet("shift-assignments")]
        public async Task<ActionResult<BrandOverview>> GetShiftAssignmentsOfStore(
            [FromQuery] ShiftAssignmentParams @params)
        {
            try
            {
                int storeId = int.Parse(User.GetStoreId());
                var shiftAssignments = await _shiftAssignmentService
                    .GetShiftAssignments(storeId, @params);
                Response.AddPaginationHeader(shiftAssignments);

                return Ok(shiftAssignments);
            }
            catch (AppException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
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

        [HttpGet("shift-attendances")]
        public async Task<ActionResult<BrandOverview>> GetShiftAttendancesOfStore(
            [FromQuery] ShiftAttendanceParams @params)
        {
            try
            {
                int storeId = int.Parse(User.GetStoreId());
                var shiftAttendances = await _shiftAttendanceService
                    .GetShiftAttendencesAsync(storeId, @params);
                Response.AddPaginationHeader(shiftAttendances);

                return Ok(shiftAttendances);
            }
            catch (AppException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
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
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
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

        [HttpPost("assign/staff")]
        public async Task<ActionResult> AssignStaff(StoreAssign storeAssign)
        {
            try
            {
                return Ok(await _storeStaffService.AssignStaff(storeAssign));
            }
            catch (AppException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
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

        [HttpPut]
        public async Task<ActionResult> UpdateStore(
            StoreUpdate storeUpdate)
        {
            try
            {
                int storeId = int.Parse(User.GetStoreId());
                await _storeService.UpdateStore(storeId, storeUpdate);
            }
            catch (AppException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
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
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
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

            return NoContent();
        }
    }
}
