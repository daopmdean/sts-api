using System;
using System.Collections.Generic;
using Data.Entities;
using Data.Models.Requests;

namespace Service.Helpers
{
    public class Scheduling
    {
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
