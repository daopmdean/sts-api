using System;
using System.ComponentModel.DataAnnotations;

namespace Service.Models
{
    public class RegisterInfo
    {
        [Required]
        public string Username { get; set; }

        [MinLength(6)]
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
