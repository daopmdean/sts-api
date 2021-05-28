using Data.Entities;

namespace Data.Models.Responses
{
    public class UserOverview
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public RoleResponse Role { get; set; }
    }
}
