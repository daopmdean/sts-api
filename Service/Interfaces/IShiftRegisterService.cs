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
        Task<ShiftRegister> GetShiftRegister(int id);
        Task<ShiftRegister> CreateShiftRegister(ShiftRegistersCreate create);
        Task UpdateShiftRegister(int id, ShiftRegisterUpdate update);
        Task DeleteShiftRegister(int id);
    }
}
