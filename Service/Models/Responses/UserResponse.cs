﻿using System;
namespace Service.Models.Responses
{
    public class UserResponse
    {
        public int Status { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}