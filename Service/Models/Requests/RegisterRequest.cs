using System;
using System.ComponentModel.DataAnnotations;

namespace Service.Models.Requests
{
    public class RegisterRequest
    {
        [MinLength(4)]
        public string Username { get; set; }

        [MinLength(6)]
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
    }
}
