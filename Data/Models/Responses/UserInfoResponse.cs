using System;
using Data.Enums;

namespace Data.Models.Responses
{
    public class UserInfoResponse
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public string Phone { get; set; }

        public int Gender { get; set; }

        public StaffType? Type { get; set; }

        public int BrandId { get; set; }

        public string Address { get; set; }

        public string PhotoUrl { get; set; }
    }
}
