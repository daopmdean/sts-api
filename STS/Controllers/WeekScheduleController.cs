using System;
using System.Threading.Tasks;
using Data.Entities;
using Data.Enums;
using Data.Models.Requests;
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
        private readonly IShiftRegisterService _shiftRegisterService;

        public WeekScheduleController(IWeekScheduleService weekScheduleService,
            IWeekScheduleDetailService weekScheduleDetailService,
            IStoreScheduleDetailService storeScheduleDetailService,
            IStaffScheduleDetailService staffScheduleDetailService,
            IStoreStaffService storeStaffService,
            IShiftRegisterService shiftRegisterService)
        {
            _weekScheduleService = weekScheduleService;
            _weekScheduleDetailService = weekScheduleDetailService;
            _storeScheduleDetailService = storeScheduleDetailService;
            _staffScheduleDetailService = staffScheduleDetailService;
            _storeStaffService = storeStaffService;
            _shiftRegisterService = shiftRegisterService;
        }

        [HttpGet]
        public async Task<ActionResult> GetWeekScheduleByDate(
            [FromQuery] DateTime dateStart, string status)
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

                var weekStatus = Status.Register;
                switch (status)
                {
                    case "unpublished":
                        weekStatus = Status.Unpublished;
                        break;
                    case "published":
                        weekStatus = Status.Published;
                        break;
                }

                var username = User.GetUsername();
                var weekSchedule = await _weekScheduleService
                    .GetWeekScheduleAsync(storeId, dateStart, weekStatus, username);
                return Ok(weekSchedule);
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
        public async Task<ActionResult> GetWeekSchedule(
            int id)
        {
            try
            {
                return Ok(await _weekScheduleService.GetWeekScheduleAsync(id));
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
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
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
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
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
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpGet("{weekScheduleId}/shift-registers")]
        public async Task<ActionResult> GetShiftRegisters(
            int weekScheduleId)
        {
            try
            {
                var shiftRegisters = await _shiftRegisterService
                    .GetShiftRegisters(weekScheduleId);

                return Ok(shiftRegisters);
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
        public async Task<ActionResult<WeekSchedule>> CreateWeekSchedule(
            WeekScheduleCreate weekSchedule)
        {
            try
            {
                return Ok(await _weekScheduleService.CreateWeekScheduleAsync(weekSchedule));
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

        [HttpPost("clone")]
        public async Task<ActionResult<WeekSchedule>> CloneWeekSchedule(
            WeekScheduleCloneRequest clone)
        {
            try
            {
                return Ok(await _weekScheduleService
                    .CloneWeekScheduleAsync(clone));
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
        public async Task<ActionResult<WeekSchedule>> UpdateWeekSchedule(
            int id, WeekScheduleUpdate update)
        {
            try
            {
                await _weekScheduleService
                    .UpdateWeekScheduleAsync(id, update);
                return NoContent();
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<WeekSchedule>> DeleteWeekSchedule(
            int id)
        {
            try
            {
                await _weekScheduleService.DeleteWeekScheduleAsync(id);
                return NoContent();
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
    }
}
