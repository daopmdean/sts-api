using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;

namespace Service.Interfaces
{
    public interface IStoreScheduleDetailService
    {
        Task<IEnumerable<StoreScheduleDetail>> GetStoreScheduleDetails(
            int weekScheduleId);
        Task<StoreScheduleDetail> GetStoreScheduleDetail(int id);
        Task<StoreScheduleDetail> CreateStoreScheduleDetailAsync(
            StoreScheduleDetailCreate create);
        Task UpdateStoreScheduleDetailAsync(int id,
            StoreScheduleDetailUpdate update);
        Task DeleteStoreScheduleDetailAsync(int id);
    }
}
