using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class User
    {
        [Key]
        public string Username { get; set; }

        public string Email { get; set; }

        public byte[] Password { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Phone { get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }

        public string PhotoUrl { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
