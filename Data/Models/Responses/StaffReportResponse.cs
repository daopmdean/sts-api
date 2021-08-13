using System;
using System.Collections.Generic;

namespace Data.Models.Responses
{
    public class StaffReportResponse
    {
        public double TotalWorkHours { get; set; } = 0;
        public double TotalAssignedHours { get; set; } = 0;
        public int TotalArriveLate { get; set; } = 0;
        public int TotalLeaveEarly { get; set; } = 0;
        public int TotalAbsent { get; set; } = 0;
        public int TotalLackCheckIn { get; set; } = 0;
        public int TotalLackCheckOut { get; set; } = 0;
        public IEnumerable<StaffDetailReportResponse> Records { get; set; }

        public StaffReportResponse(
            IEnumerable<ShiftAssignmentOverview> assignments)
        {
            List<StaffDetailReportResponse> list = new();
            foreach (var assignment in assignments)
            {
                var staffDetailReport =
                    new StaffDetailReportResponse(assignment);
                list.Add(staffDetailReport);
            }

            foreach (var item in list)
            {
                TotalAssignedHours += item.AssignedHours;
                TotalWorkHours += item.WorkHours;
                if (item.ArrivedLate)
                    TotalArriveLate += 1;
                if (item.LeftEarly)
                    TotalLeaveEarly += 1;
                if (item.Absent)
                    TotalAbsent += 1;
                if (item.LackCheckIn)
                    TotalLackCheckIn += 1;
                if (item.LackCheckOut)
                    TotalLackCheckOut += 1;
            }

            Records = list;
        }
    }

    public class StaffDetailReportResponse
    {
        public int StoreId { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public DateTime TimeCheckIn { get; set; }
        public DateTime TimeCheckOut { get; set; }
        public DateTime Date { get; set; }
        public float WorkHours { get; set; } = 0;
        public float AssignedHours { get; set; } = 0;
        public bool ArrivedLate { get; set; } = false;
        public bool LeftEarly { get; set; } = false;
        public bool Absent { get; set; } = false;
        public bool LackCheckIn { get; set; } = false;
        public bool LackCheckOut { get; set; } = false;

        public StaffDetailReportResponse(
            ShiftAssignmentOverview assignment)
        {
            StoreId = assignment.StoreId;
            TimeStart = assignment.TimeStart;
            TimeEnd = assignment.TimeEnd;
            TimeCheckIn = assignment.TimeCheckIn;
            TimeCheckOut = assignment.TimeCheckOut;
            Date = new DateTime(TimeStart.Year, TimeStart.Month, TimeStart.Day);

            TimeSpan assignedTime = TimeEnd - TimeStart;
            AssignedHours = (float)assignedTime.TotalHours;

            if (TimeCheckIn != DateTime.MinValue
                && TimeCheckOut != DateTime.MinValue)
            {
                TimeSpan time = TimeCheckOut - TimeCheckIn;
                WorkHours = (float)time.TotalHours;
            }

            if (TimeCheckIn != DateTime.MinValue
                && TimeCheckIn > TimeStart)
            {
                ArrivedLate = true;
            }

            if (TimeCheckOut != DateTime.MinValue
                && TimeCheckOut < TimeEnd)
            {
                LeftEarly = true;
            }

            if (DateTime.Now > TimeEnd
                && TimeCheckIn == DateTime.MinValue
                && TimeCheckOut == DateTime.MinValue)
            {
                Absent = true;
            }
            else if (DateTime.Now > TimeEnd
                && TimeCheckIn == DateTime.MinValue
                && TimeCheckOut != DateTime.MinValue)
            {
                LackCheckIn = true;
            }
            else if (DateTime.Now > TimeEnd
                && TimeCheckIn != DateTime.MinValue
                && TimeCheckOut == DateTime.MinValue)
            {
                LackCheckOut = true;
            }
        }
    }
}
