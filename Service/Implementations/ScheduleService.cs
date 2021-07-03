﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Service.Enums;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations
{
    public class ScheduleService : IScheduleService
    {
        private readonly IShiftRegisterService _shiftRegisterService;
        private readonly IWeekScheduleDetailService _weekScheduleDetailService;
        private readonly IStoreScheduleDetailService _storeScheduleDetailService;

        public ScheduleService(
            IShiftRegisterService shiftRegisterService,
            IWeekScheduleDetailService weekScheduleDetailService,
            IStoreScheduleDetailService storeScheduleDetailService)
        {
            _shiftRegisterService = shiftRegisterService;
            _weekScheduleDetailService = weekScheduleDetailService;
            _storeScheduleDetailService = storeScheduleDetailService;
        }

        public async Task<ScheduleResponse> ComputeSchedule(
            ScheduleRequest request)
        {
            HttpClient client = new();
            client.BaseAddress = new Uri("https://localhost:5001/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/schedule", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content
                    .ReadFromJsonAsync<ScheduleResponse>();
                return result;
            }

            throw new AppException((int)StatusCode.BadRequest,
                "Can not compute schedule");
        }
    }
}
