namespace Data.Models.Responses
{
    public class UserOverview
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public RoleResponse Role { get; set; }
    }
}
