using System;
namespace Data.Pagings
{
    public class UserParams : PaginationParams
    {
        public string Position { get; set; }
        public int BrandId { get; set; }
        public int StoreId { get; set; }
    }
}
