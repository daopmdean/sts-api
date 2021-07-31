using System;
using System.Threading.Tasks;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Helpers;
using Service.Interfaces;
using STS.Extensions;

namespace STS.Controllers
{
    [Authorize(Policy = "RequiredManagers")]
    [Route("api/manager")]
    public class ManagerController : ApiBaseController
    {
        private readonly IManagerService _managerService;
        private readonly IScheduleService _scheduleService;
        private readonly IShiftScheduleResultService _scheduleResultService;
        private readonly IStoreStaffService _storeStaffService;
        private readonly IStaffSkillService _staffSkillService;
        private readonly IWeekScheduleService _weekService;
        private readonly IStoreService _storeService;
        private readonly IAttendanceService _attendanceService;

        public ManagerController(
            IManagerService managerService,
            IScheduleService scheduleService,
            IShiftScheduleResultService scheduleResultService,
            IStoreStaffService storeStaffService,
            IStaffSkillService staffSkillService,
            IWeekScheduleService weekService,
            IStoreService storeService,
            IAttendanceService attendanceService)
        {
            _managerService = managerService;
            _scheduleService = scheduleService;
            _scheduleResultService = scheduleResultService;
            _storeStaffService = storeStaffService;
            _staffSkillService = staffSkillService;
            _weekService = weekService;
            _storeService = storeService;
            _attendanceService = attendanceService;
        }

        [HttpGet("brands/report")]
        public async Task<ActionResult> GetBrandReport(
            [FromQuery] DateTimeParams @params)
        {
            try
            {
                await _managerService
                    .GetBrandReport(@params);

                return Ok();
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

        [HttpGet("stores/report")]
        public async Task<ActionResult> GetStoreReport(
            [FromQuery] DateTimeParams @params)
        {
            try
            {
                await _managerService
                    .GetStoreReport(@params);

                return Ok();
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

        [HttpGet("stores/{storeId}/week-schedules")]
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
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpGet("stores/{storeId}/staff")]
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
                return BadRequestResponse(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResponse(ex);
            }
        }

        [HttpPost("users/store-manager")]
        public async Task<IActionResult> RegisterStoreManager(
            StoreManagerCreate info)
        {
            try
            {
                var brandId = int.Parse(User.GetBrandId());

                return Ok(await _managerService
                    .CreateStoreManager(brandId, info));
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

        [HttpPost("users/staff")]
        public async Task<IActionResult> RegisterStaff(StaffCreate info)
        {
            try
            {
                var brandId = int.Parse(User.GetBrandId());

                return Ok(await _managerService
                    .CreateStaff(brandId, info));
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

        [HttpPut("users/staff")]
        public async Task<IActionResult> UpdateStaff(StaffUpdate info)
        {
            try
            {
                await _managerService.UpdateStaff(info);
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

        [HttpGet("users/{username}/skills")]
        public async Task<ActionResult> GetSkillsOfUser(
            string username, [FromQuery] StaffSkillParams @params)
        {
            try
            {
                var skills = await _staffSkillService
                    .GetSkillsFromStaffAsync(username, @params);
                Response.AddPaginationHeader(skills);

                return Ok(skills);
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

        [HttpPost("users/attendances")]
        public async Task<IActionResult> GetStaffAttendances(
            AttendanceManualCreate create)
        {
            try
            {
                return Ok(await _attendanceService
                    .CreateAttendanceManualAsync(create));
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

        [HttpGet("users/{username}/attendances")]
        public async Task<IActionResult> GetStaffAttendances(
            string username, DateTimeParams @params)
        {
            try
            {
                return Ok(await _attendanceService
                    .GetAttendancesAsync(username, @params));
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

        [HttpPost("stores/calculate-work-time")]
        public async Task<ActionResult> CalculateWorkTime(
            [FromQuery] DateTimeParams @params)
        {
            try
            {
                int storeId = int.Parse(User.GetStoreId());
                await _managerService
                    .CalculateWorkTime(storeId, @params, 15);

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

        [HttpPost("schedule")]
        public async Task<IActionResult> ComputeSchedule(
            ComputeScheduleRequest request)
        {
            try
            {
                var brandId = int.Parse(User.GetBrandId());
                return Ok(await _scheduleService
                    .ComputeSchedule(request.WeekScheduleId, brandId));
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

        [HttpPost("schedule/publish")]
        public async Task<ActionResult> PublishSchedule(
            PublishInfo create)
        {
            try
            {
                return Ok(await _managerService
                    .PublishSchedule(create));
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

        [HttpPost("schedule/unpublish")]
        public async Task<ActionResult> UnpublishSchedule(
            UnpublishInfo create)
        {
            try
            {
                await _managerService.UnpublishSchedule(create);
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

        [HttpPost("schedule/result")]
        public async Task<IActionResult> GetScheduleResult(
            ShiftScheduleRequest request)
        {
            try
            {
                return Ok(await _scheduleResultService
                    .GetScheduleResult(request.ShiftScheduleResultId));
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

        [HttpPost("schedule/data")]
        public async Task<IActionResult> GetScheduleRequest(
            ComputeScheduleRequest request)
        {
            try
            {
                var brandId = int.Parse(User.GetBrandId());
                return Ok(await _scheduleService
                    .GetScheduleRequest(request.WeekScheduleId, brandId));
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

        [HttpPost("assign/store-manager")]
        public async Task<IActionResult> AssignStoreManager(
            StoreAssign info)
        {
            try
            {
                return Ok(await _storeStaffService
                    .AssignStoreManager(info));
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

        [HttpPost("assign/staff")]
        public async Task<ActionResult> AssignStaff(
            StoreAssign storeAssign)
        {
            try
            {
                return Ok(await _storeStaffService
                    .AssignStaff(storeAssign));
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

        [HttpPut("stores/{id}")]
        public async Task<ActionResult> UpdateStore(
            int id, StoreUpdate storeUpdate)
        {
            try
            {
                await _storeService.UpdateStore(id, storeUpdate);
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

        [HttpPost("notification/testing")]
        public async Task<ActionResult> NotificationTesting(
            NotificationTopicRequest request)
        {
            try
            {
                await FCMNotification
                    .SendNotificationAsync(request.Topic, request.Title, request.Message);
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