namespace Data.Models.Requests
{
    public class BrandManagerCreate
    {
        public RegisterRequest GeneralInfo { get; set; }

        public BrandCreate Brand { get; set; }
    }
}
