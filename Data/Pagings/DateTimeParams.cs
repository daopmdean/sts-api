using System;
namespace Data.Pagings
{
    public class DateTimeParams
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; } = new DateTime(9999, 12, 31);
    }
}
