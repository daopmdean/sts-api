using System;

namespace Data.Helpers
{
    public class Helper
    {
        public static void TransformDateStart(ref DateTime dateStart)
        {
            DayOfWeek currentDayOfWeek = dateStart.DayOfWeek;
            switch (currentDayOfWeek)
            {
                case DayOfWeek.Monday:
                    dateStart = dateStart.AddDays(0);
                    break;
                case DayOfWeek.Tuesday:
                    dateStart = dateStart.AddDays(-1);
                    break;
                case DayOfWeek.Wednesday:
                    dateStart = dateStart.AddDays(-2);
                    break;
                case DayOfWeek.Thursday:
                    dateStart = dateStart.AddDays(-3);
                    break;
                case DayOfWeek.Friday:
                    dateStart = dateStart.AddDays(-4);
                    break;
                case DayOfWeek.Saturday:
                    dateStart = dateStart.AddDays(-5);
                    break;
                case DayOfWeek.Sunday:
                    dateStart = dateStart.AddDays(-6);
                    break;
            }
        }
    }
}
