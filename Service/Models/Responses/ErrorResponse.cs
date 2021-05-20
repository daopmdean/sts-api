using System;
namespace Service.Models.Responses
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
