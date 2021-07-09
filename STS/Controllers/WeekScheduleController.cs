using System;
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
    [Route("api/week-schedules")]
    public class WeekScheduleController : ApiBaseController
    {
        private readonly IWeekScheduleService _weekScheduleService;
        private readonly IWeekScheduleDetailService _weekScheduleDetailService;
        private readonly IStoreScheduleDetailService _storeScheduleDetailService;
        private readonly IStaffScheduleDetailService _staffScheduleDetailService;
        private readonly IStoreStaffService _storeStaffService;

        public WeekScheduleController(IWeekScheduleService weekScheduleService,
            IWeekScheduleDetailService weekScheduleDetailService,
            IStoreScheduleDetailService storeScheduleDetailService,
            IStaffScheduleDetailService staffScheduleDetailService,
            IStoreStaffService storeStaffService)
        {
            _weekScheduleService = weekScheduleService;
            _weekScheduleDetailService = weekScheduleDetailService;
            _storeScheduleDetailService = storeScheduleDetailService;
            _staffScheduleDetailService = staffScheduleDetailService;
            _storeStaffService = storeStaffService;
        }

        [HttpGet]
        public async Task<ActionResult> GetWeekScheduleByDate(
            [FromQuery] DateTime dateStart)
        {
            try
            {
                var role = User.GetRoleName();
                var storeId = 0;
                if (role == "store manager")
                {
                    storeId = int.Parse(User.GetStoreId());
                }
                else if (role == "staff")
                {
                    storeId = await _storeStaffService
                        .GetStaffStoreIdAsync(User.GetUsername());
                }
                
                var weekSchedule = await _weekScheduleService
                    .GetWeekScheduleAsync(storeId, dateStart);
                return Ok(weekSchedule);
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
        public async Task<ActionResult> GetWeekSchedule(
            int id)
        {
            try
            {
                return Ok(await _weekScheduleService.GetWeekScheduleAsync(id));
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

        [HttpGet("{weekScheduleId}/week-schedule-details")]
        public async Task<ActionResult> GetWeekScheduleDetails(
            int weekScheduleId)
        {
            try
            {
                var weekScheduleDetails = await _weekScheduleDetailService
                    .GetWeekScheduleDetailsAsync(weekScheduleId); ;

                return Ok(weekScheduleDetails);
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

        [HttpGet("{weekScheduleId}/store-schedule-details")]
        public async Task<ActionResult> GetStoreScheduleDetails(
            int weekScheduleId)
        {
            try
            {
                var storeScheduleDetails = await _storeScheduleDetailService
                    .GetStoreScheduleDetails(weekScheduleId);

                return Ok(storeScheduleDetails);
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

        [HttpGet("{weekScheduleId}/staff-schedule-details")]
        public async Task<ActionResult> GetStaffScheduleDetails(
            int weekScheduleId)
        {
            try
            {
                var staffScheduleDetails = await _staffScheduleDetailService
                    .GetStaffScheduleDetails(weekScheduleId);

                return Ok(staffScheduleDetails);
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
        public async Task<ActionResult<WeekSchedule>> CreateWeekSchedule(
            WeekScheduleCreate weekSchedule)
        {
            try
            {
                return Ok(await _weekScheduleService.CreateWeekScheduleAsync(weekSchedule));
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
    }
}
