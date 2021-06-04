using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models.Requests
{
    public class UserUpdate
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public string Gender { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
    }
}
