using AutoMapper;
using Data.Entities;
using Data.Enums;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Repositories.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Service.Helpers;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class ScheduleService : IScheduleService
    {
        private readonly IShiftScheduleResultService _shiftScheduleResultService;
        private readonly IShiftRegisterService _shiftRegisterService;
        private readonly IWeekScheduleService _weekScheduleService;
        private readonly IWeekScheduleDetailService _weekScheduleDetailService;
        private readonly IStoreScheduleDetailService _storeScheduleDetailService;
        private readonly IStaffSkillService _staffSkillService;
        private readonly ISkillService _skillService;
        private readonly IUserRepository _userRepository;
        private readonly IModel _rabbitMqChannel;
        private readonly IMapper _mapper;

        public ScheduleService(
            IShiftScheduleResultService shiftScheduleResultService,
            IShiftRegisterService shiftRegisterService,
            IWeekScheduleService weekScheduleService,
            IWeekScheduleDetailService weekScheduleDetailService,
            IStoreScheduleDetailService storeScheduleDetailService,
            IStaffSkillService staffSkillService,
            ISkillService skillService,
            IUserRepository userRepository,
            IModel rabbitMqChannel,
            IMapper mapper)
        {
            _shiftScheduleResultService = shiftScheduleResultService;
            _shiftRegisterService = shiftRegisterService;
            _weekScheduleService = weekScheduleService;
            _weekScheduleDetailService = weekScheduleDetailService;
            _storeScheduleDetailService = storeScheduleDetailService;
            _staffSkillService = staffSkillService;
            _skillService = skillService;
            _userRepository = userRepository;
            _rabbitMqChannel = rabbitMqChannel;
            _mapper = mapper;
        }

        public async Task<ShiftScheduleResult> ComputeSchedule(
            int weekScheduleId, int brandId)
        {
            ScheduleRequest request = await GetScheduleRequest(
                weekScheduleId, brandId);

            var result = await _shiftScheduleResultService
                .CreateShiftScheduleResult(weekScheduleId);
            request.Id = result.Id;
            var message = JsonConvert.SerializeObject(request);
            var body = Encoding.UTF8.GetBytes(message);
            var properties = _rabbitMqChannel.CreateBasicProperties();

            properties.Persistent = true;
            _rabbitMqChannel.BasicPublish("", "sts_api_request", properties, body);

            return result;
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
                        Scheduling.SwitchDayOfWeek(staff, shiftRegister);
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

                    Scheduling
                        .SwitchDayOfWeekFirstCreate(
                        staffRequestDatas, shiftRegister);
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

        public async Task<ScheduleRequest> GetScheduleRequest(
            int weekScheduleId, int brandId)
        {
            ScheduleRequest request = new();
            request.WeekScheduleId = weekScheduleId;

            var weekSchedule = await _weekScheduleService
                    .GetWeekScheduleAsync(weekScheduleId);
            request.DateStart = weekSchedule.DateStart;
            request.StoreId = weekSchedule.StoreId;

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


            var weekRegister = (await _weekScheduleService
                .GetWeekScheduleAsync(weekSchedule.StoreId,
                    weekSchedule.DateStart, Status.Register))
                .First();
            var shiftRegisters = await _shiftRegisterService
                .GetShiftRegisters(weekRegister.Id);
            var staffRequests = await ConvertToStaffs(shiftRegisters);
            request.Staffs = staffRequests;

            return request;
        }
    }
}
