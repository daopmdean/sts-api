using System;
using System.Collections.Generic;

namespace Data.Models.Requests
{
    public class StaffUpdate
    {
        public RegisterRequest GeneralInfo { get; set; }

        public StoreStaffCreate JobInformation { get; set; }

        public ICollection<StaffSkillCreate> StaffSkills { get; set; }
    }

    public class StaffUpdateRequest
    {

    }
}
