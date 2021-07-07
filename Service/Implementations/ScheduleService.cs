using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Data.Enums;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Repositories.Interfaces;
using Service.Enums;
using Service.Exceptions;
using Service.Helpers;
using Service.Interfaces;

namespace Service.Implementations
{
    public class ScheduleService : IScheduleService
    {
        private readonly IShiftRegisterService _shiftRegisterService;
        private readonly IWeekScheduleDetailService _weekScheduleDetailService;
        private readonly IStoreScheduleDetailService _storeScheduleDetailService;
        private readonly IStaffSkillService _staffSkillService;
        private readonly ISkillService _skillService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ScheduleService(
            IShiftRegisterService shiftRegisterService,
            IWeekScheduleDetailService weekScheduleDetailService,
            IStoreScheduleDetailService storeScheduleDetailService,
            IStaffSkillService staffSkillService,
            ISkillService skillService,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _shiftRegisterService = shiftRegisterService;
            _weekScheduleDetailService = weekScheduleDetailService;
            _storeScheduleDetailService = storeScheduleDetailService;
            _staffSkillService = staffSkillService;
            _skillService = skillService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ScheduleResponse> ComputeSchedule(
            int weekScheduleId, int brandId)
        {
            HttpClient client = new();
            client.BaseAddress = new Uri("https://sts-schedule.herokuapp.com/");
            //client.BaseAddress = new Uri("https://localhost:5001/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            ScheduleRequest request = new();

            var shiftRegisters = await _shiftRegisterService
                .GetShiftRegisters(weekScheduleId);
            var staffRequests = await ConvertToStaffs(shiftRegisters);
            request.Staffs = staffRequests;

            var weekScheduleDetails = await _weekScheduleDetailService
                .GetWeekScheduleDetailsAsync(weekScheduleId);
            var demands = Scheduling.ConvertToDemands(weekScheduleDetails);
            request.Demands = demands;

            var storeScheduleDetails = await _storeScheduleDetailService
                .GetStoreScheduleDetails(weekScheduleId);
            var contraint = ConvertToContraints(storeScheduleDetails);
            request.Constraints = contraint;

            var skills = await _skillService.GetSkills(brandId);
            var skillRequests = ConvertToSkills(skills);
            request.Skills = skillRequests;

            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/scheduling/testing", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content
                    .ReadFromJsonAsync<ScheduleResponse>();
                return result;
            }

            throw new AppException((int)StatusCode.BadRequest,
                "Can not compute schedule");
        }

        private List<SkillRequest> ConvertToSkills(
            IEnumerable<SkillOverview> skillOverviews)
        {
            List<SkillRequest> skillRequests = new();
            foreach (var skill in skillOverviews)
            {
                skillRequests.Add(_mapper.Map<SkillRequest>(skill));
            }
            return skillRequests;
        }

        private async Task<List<StaffRequestData>> ConvertToStaffs(
            IEnumerable<ShiftRegister> shiftRegisters)
        {
            List<StaffRequestData> staffRequestDatas = new();

            foreach (var shiftRegister in shiftRegisters)
            {
                bool staffNotFound = true;
                foreach (var staff in staffRequestDatas)
                {
                    if (staff.Username == shiftRegister.Username)
                    {
                        staffNotFound = false;
                        break;
                    }
                }
                if (staffNotFound)
                {
                    var staffType = (int)await _userRepository
                        .GetStaffTypeAsync(shiftRegister.Username);

                    var staffSkills = await _staffSkillService
                        .GetSkillsFromStaffAsync(shiftRegister.Username);
                    List<SkillStaff> skillStaffs = new();

                    foreach (var item in staffSkills)
                    {
                        skillStaffs.Add(_mapper.Map<SkillStaff>(item));
                    }

                    List<AvalailableDayRequest> avalailables = new();
                    Scheduling.InitializedAvalailableDays(avalailables);

                    staffRequestDatas.Add(new StaffRequestData
                    {
                        Username = shiftRegister.Username,
                        TypeStaff = staffType,
                        Skills = skillStaffs,
                        AvalailableDays = avalailables
                    });

                    switch (shiftRegister.TimeStart.DayOfWeek)
                    {
                        case DayOfWeek.Monday:

                            break;
                        case DayOfWeek.Tuesday:
                            break;
                        case DayOfWeek.Wednesday:
                            break;
                        case DayOfWeek.Thursday:
                            break;
                        case DayOfWeek.Friday:
                            break;
                        case DayOfWeek.Saturday:
                            break;
                        case DayOfWeek.Sunday:
                            break;
                    }
                }
            }

            return staffRequestDatas;
        }


        private ConstraintData ConvertToContraints(
            IEnumerable<StoreScheduleDetail> storeScheduleDetails)
        {
            ConstraintData constraint = new();

            foreach (var item in storeScheduleDetails)
            {
                switch (item.StaffType)
                {
                    case StaffType.FullTime:
                        constraint.FulltimeConstraints =
                            _mapper.Map<ConstraintSpecific>(item);
                        break;
                    case StaffType.PartTime:
                        constraint.ParttimeConstraints =
                            _mapper.Map<ConstraintSpecific>(item);
                        break;
                }
            }

            constraint.MinDistanceBetweenSession = 0;

            return constraint;
        }

        public async Task<ScheduleRequest> Testing(
            int weekScheduleId, int brandId)
        {
            ScheduleRequest request = new();

            var shiftRegisters = await _shiftRegisterService
                .GetShiftRegisters(weekScheduleId);
            var staffRequests = await ConvertToStaffs(shiftRegisters);
            request.Staffs = staffRequests;

            var weekScheduleDetails = await _weekScheduleDetailService
                .GetWeekScheduleDetailsAsync(weekScheduleId);
            var demands = Scheduling.ConvertToDemands(weekScheduleDetails);
            request.Demands = demands;

            var storeScheduleDetails = await _storeScheduleDetailService
                .GetStoreScheduleDetails(weekScheduleId);
            var contraint = ConvertToContraints(storeScheduleDetails);
            request.Constraints = contraint;

            var skills = await _skillService.GetSkills(brandId);
            var skillRequests = ConvertToSkills(skills);
            request.Skills = skillRequests;

            return request;
        }
    }
}
