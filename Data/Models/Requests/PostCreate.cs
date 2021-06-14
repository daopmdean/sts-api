namespace Data.Models.Requests
{
    public class PostCreate
    {
        public int BrandId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }
    }
}
