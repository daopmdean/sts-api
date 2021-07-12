using Data.Models.Responses;
using System;
namespace Service.Exceptions
{
    public class AppException : Exception
    {
        public int StatusCode { get; set; }

        public AppException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public AppException(ErrorResponse error) : base(error.Message)
        {
            StatusCode = error.StatusCode;
        }
    }
}
