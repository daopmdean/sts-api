using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ShiftScheduleResult : BaseEntity
    {
        public long Id { get; set; }
        public bool IsComplete { get; set; } = false;
        public long Conflicts { get; set; }
        public long Branches { get; set; }
        public double WallTime { get; set; }
        public ICollection<ShiftScheduleDetailResult> ShiftScheduleDetailResults { get; set; }

    }
}
