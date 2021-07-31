using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models.Requests
{
    public class StaffUpdate
    {
        public StaffUpdateRequest GeneralInfo { get; set; }

        public StoreStaffCreate JobInformation { get; set; }

        public ICollection<StaffSkillCreate> StaffSkills { get; set; }
    }

    public class StaffUpdateRequest
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public int Gender { get; set; }
        public string Type { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
