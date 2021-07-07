using System;
using System.Collections.Generic;
using Data.Entities;
using Data.Models.Requests;

namespace Service.Helpers
{
    public class Scheduling
    {
        public static void InitializedAvalailableDays(
            List<AvalailableDayRequest> avalailables)
        {
            for (int i = 0; i < 7; i++)
            {
                avalailables.Add(new AvalailableDayRequest
                {
                    Day = i,
                    AvailableTimes = new()
                });
            }
        }

        public static void SwitchDayOfWeek(StaffRequestData staff,
            ShiftRegister shiftRegister)
        {
            switch (shiftRegister.TimeStart.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    AddShiftRegisterToStaff(staff, shiftRegister, 0);
                    break;
                case DayOfWeek.Tuesday:
                    AddShiftRegisterToStaff(staff, shiftRegister, 1);
                    break;
                case DayOfWeek.Wednesday:
                    AddShiftRegisterToStaff(staff, shiftRegister, 2);
                    break;
                case DayOfWeek.Thursday:
                    AddShiftRegisterToStaff(staff, shiftRegister, 3);
                    break;
                case DayOfWeek.Friday:
                    AddShiftRegisterToStaff(staff, shiftRegister, 4);
                    break;
                case DayOfWeek.Saturday:
                    AddShiftRegisterToStaff(staff, shiftRegister, 5);
                    break;
                case DayOfWeek.Sunday:
                    AddShiftRegisterToStaff(staff, shiftRegister, 6);
                    break;
            }
        }

        public static void SwitchDayOfWeekFirstCreate(
            List<StaffRequestData> staffRequestDatas,
            ShiftRegister shiftRegister)
        {
            switch (shiftRegister.TimeStart.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    foreach (var staff in staffRequestDatas)
                    {
                        if (staff.Username == shiftRegister.Username)
                        {
                            AddShiftRegisterToStaff(staff, shiftRegister, 0);
                            break;
                        }
                    }
                    break;
                case DayOfWeek.Tuesday:
                    foreach (var staff in staffRequestDatas)
                    {
                        if (staff.Username == shiftRegister.Username)
                        {
                            AddShiftRegisterToStaff(staff, shiftRegister, 1);
                            break;
                        }
                    }
                    break;
                case DayOfWeek.Wednesday:
                    foreach (var staff in staffRequestDatas)
                    {
                        if (staff.Username == shiftRegister.Username)
                        {
                            AddShiftRegisterToStaff(staff, shiftRegister, 2);
                            break;
                        }
                    }
                    break;
                case DayOfWeek.Thursday:
                    foreach (var staff in staffRequestDatas)
                    {
                        if (staff.Username == shiftRegister.Username)
                        {
                            AddShiftRegisterToStaff(staff, shiftRegister, 3);
                            break;
                        }
                    }
                    break;
                case DayOfWeek.Friday:
                    foreach (var staff in staffRequestDatas)
                    {
                        if (staff.Username == shiftRegister.Username)
                        {
                            AddShiftRegisterToStaff(staff, shiftRegister, 4);
                            break;
                        }
                    }
                    break;
                case DayOfWeek.Saturday:
                    foreach (var staff in staffRequestDatas)
                    {
                        if (staff.Username == shiftRegister.Username)
                        {
                            AddShiftRegisterToStaff(staff, shiftRegister, 5);
                            break;
                        }
                    }
                    break;
                case DayOfWeek.Sunday:
                    foreach (var staff in staffRequestDatas)
                    {
                        if (staff.Username == shiftRegister.Username)
                        {
                            AddShiftRegisterToStaff(staff, shiftRegister, 6);
                            break;
                        }
                    }
                    break;
            }
        }

        private static void AddShiftRegisterToStaff(StaffRequestData staff,
            ShiftRegister shiftRegister, int dayOfWeek)
        {
            staff.AvalailableDays[dayOfWeek]
                .AvailableTimes
                .Add(new AvailableTimeRequest
                {
                    Start = shiftRegister.TimeStart,
                    End = shiftRegister.TimeEnd
                });
        }

        private static void InitializedDemands(List<DemandDayRequest> demands)
        {
            for (int i = 0; i < 7; i++)
            {
                demands.Add(new DemandDayRequest
                {
                    Day = i,
                    DemandBySkills = new()
                });
            }
        }

        private static void AddWeekScheduleDetailToDemands(
            List<DemandDayRequest> demands,
            WeekScheduleDetail weekScheduleDetail,
            int dayOfWeek)
        {
            bool skillNotExist = true;

            foreach (var item in demands[dayOfWeek].DemandBySkills)
            {
                if (item.SkillId == weekScheduleDetail.SkillId)
                {
                    skillNotExist = false;
                    item.Demands.Add(new DemandRequest
                    {
                        Quantity = weekScheduleDetail.Quantity,
                        Start = weekScheduleDetail.WorkStart,
                        End = weekScheduleDetail.WorkEnd
                    });
                }
            }
            if (skillNotExist)
            {
                demands[dayOfWeek].DemandBySkills.Add(new DemandSkillRequest
                {
                    SkillId = weekScheduleDetail.SkillId,
                    Demands = new()
                });
                var count = demands[dayOfWeek].DemandBySkills.Count;
                demands[dayOfWeek].DemandBySkills[count - 1].Demands.Add(
                    new DemandRequest
                    {
                        Quantity = weekScheduleDetail.Quantity,
                        Start = weekScheduleDetail.WorkStart,
                        End = weekScheduleDetail.WorkEnd
                    });
            }
        }

        public static List<DemandDayRequest> ConvertToDemands(
            IEnumerable<WeekScheduleDetail> weekSchedules)
        {
            List<DemandDayRequest> demands = new();

            InitializedDemands(demands);

            foreach (var weekSchedule in weekSchedules)
            {
                switch (weekSchedule.WorkStart.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        AddWeekScheduleDetailToDemands(demands, weekSchedule, 0);
                        break;
                    case DayOfWeek.Tuesday:
                        AddWeekScheduleDetailToDemands(demands, weekSchedule, 1);
                        break;
                    case DayOfWeek.Wednesday:
                        AddWeekScheduleDetailToDemands(demands, weekSchedule, 2);
                        break;
                    case DayOfWeek.Thursday:
                        AddWeekScheduleDetailToDemands(demands, weekSchedule, 3);
                        break;
                    case DayOfWeek.Friday:
                        AddWeekScheduleDetailToDemands(demands, weekSchedule, 4);
                        break;
                    case DayOfWeek.Saturday:
                        AddWeekScheduleDetailToDemands(demands, weekSchedule, 5);
                        break;
                    case DayOfWeek.Sunday:
                        AddWeekScheduleDetailToDemands(demands, weekSchedule, 6);
                        break;
                }
            }

            return demands;
        }
    }
}
