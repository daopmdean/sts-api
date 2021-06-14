using System;
namespace Data.Models.Responses
{
    public class PostOverview
    {
        public int Id { get; set; }

        public int BrandId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModified { get; set; }
    }
}
