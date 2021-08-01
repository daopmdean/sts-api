using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;

namespace Service.Interfaces
{
    public interface IShiftRegisterService
    {
        Task<PagedList<ShiftRegisterOverview>> GetShiftRegisters(string username,
            ShiftRegisterParams @params);
        Task<IEnumerable<ShiftRegisterOverview>> GetShiftRegisters(string username,
            DateTimeParams @params);
        Task<IEnumerable<ShiftRegister>> GetShiftRegisters(
            int weekScheduleId);
        Task<ShiftRegister> GetShiftRegister(int id);
        Task<ShiftRegister> CreateShiftRegister(ShiftRegistersCreate create);
        Task UpdateShiftRegister(int id, ShiftRegisterUpdate update);
        Task DeleteShiftRegister(int id);
    }
}
