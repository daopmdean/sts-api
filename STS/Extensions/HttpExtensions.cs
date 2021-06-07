using System.Text.Json;
using Data.Pagings;
using Microsoft.AspNetCore.Http;

namespace STS.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response,
            int currentPage, int pageSize, int totalCount, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage,
                pageSize, totalCount, totalPages);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, options));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }

        public static void AddPaginationHeader<T>(this HttpResponse response,
            PagedList<T> source) where T : class
        {
            var paginationHeader = new PaginationHeader(source.CurrentPage,
                source.PageSize, source.TotalCount, source.TotalPages);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, options));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
