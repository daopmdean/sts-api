using System;
namespace Data.Models.Requests
{
    public class PasswordUpdate
    {
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
