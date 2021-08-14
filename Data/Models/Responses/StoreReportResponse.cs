using System;
using System.Collections.Generic;
using Data.Pagings;

namespace Data.Models.Responses
{
    public class StoreReportResponse
    {
        public double StoreTotalWorkHours { get; set; } = 0;
        public double StoreTotalAssignedHours { get; set; } = 0;
        public int StoreTotalArriveLate { get; set; } = 0;
        public int StoreTotalLeaveEarly { get; set; } = 0;
        public int StoreTotalAbsent { get; set; } = 0;
        public int StoreTotalLackCheckIn { get; set; } = 0;
        public int StoreTotalLackCheckOut { get; set; } = 0;
        public DateTimeParams TimeRange { get; set; }
        public List<StoreDetailReportResponse> Staff { get; set; }

        public StoreReportResponse(DateTimeParams @params)
        {
            TimeRange = @params;
            Staff = new();
        }

        public void AddStaff(
            StaffReportResponse staffReport,
            StoreStaffOverview staffOverview)
        {
            StoreTotalWorkHours += staffReport.TotalWorkHours;
            StoreTotalAssignedHours += staffReport.TotalAssignedHours;
            StoreTotalArriveLate += staffReport.TotalArriveLate;
            StoreTotalLeaveEarly += staffReport.TotalLeaveEarly;
            StoreTotalAbsent += staffReport.TotalAbsent;
            StoreTotalLackCheckIn += staffReport.TotalLackCheckIn;
            StoreTotalLackCheckOut += staffReport.TotalLackCheckOut;

            List<WorkDay> staffWorkDays = new();
            for (DateTime i = TimeRange.FromDate; i <= TimeRange.ToDate; i = i.AddDays(1))
            {
                float workHours = 0;

                foreach (var record in staffReport.Records)
                {
                    if (record.Date == i)
                        workHours += record.WorkHours;
                }

                staffWorkDays.Add(new WorkDay
                {
                    Date = i,
                    WorkHours = workHours
                });
            }
            Staff.Add(new StoreDetailReportResponse()
            {
                Username = staffOverview.Username,
                FirstName = staffOverview.FirstName,
                LastName = staffOverview.LastName,
                StaffTotalWorkHours = staffReport.TotalWorkHours,
                StaffTotalAssignedHours = staffReport.TotalAssignedHours,
                StaffTotalArriveLate = staffReport.TotalArriveLate,
                StaffTotalLeaveEarly = staffReport.TotalLeaveEarly,
                StaffTotalAbsent = staffReport.TotalAbsent,
                StaffTotalLackCheckIn = staffReport.TotalLackCheckIn,
                StaffTotalLackCheckOut = staffReport.TotalLackCheckOut,
                WorkDays = staffWorkDays
            });
        }
    }

    public class StoreDetailReportResponse
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double StaffTotalWorkHours { get; set; }
        public double StaffTotalAssignedHours { get; set; }
        public int StaffTotalArriveLate { get; set; }
        public int StaffTotalLeaveEarly { get; set; }
        public int StaffTotalAbsent { get; set; }
        public int StaffTotalLackCheckIn { get; set; }
        public int StaffTotalLackCheckOut { get; set; }
        public List<WorkDay> WorkDays { get; set; }
    }

    public class WorkDay
    {
        public DateTime Date { get; set; }
        public float WorkHours { get; set; } = 0;
    }
}
