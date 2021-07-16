using System.Collections.Generic;

namespace Data.Models.Responses
{
    public class UserGeneralResponse
    {
        public UserInfoResponse GeneralInfo { get; set; }

        public IEnumerable<StoreStaffOverview> JobInformations { get; set; }

        public IEnumerable<StaffSkillOverview> StaffSkills { get; set; }
    }
}
