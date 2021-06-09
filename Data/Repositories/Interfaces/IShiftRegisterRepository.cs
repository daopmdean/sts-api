using System;
using System.Threading.Tasks;
using Data.Entities;
using Data.Models.Responses;
using Data.Pagings;

namespace Data.Repositories.Interfaces
{
    public interface IShiftRegisterRepository : IBaseRepository<ShiftRegister>
    {
        Task<PagedList<ShiftRegisterOverview>> GetShiftRegistersAsync(
            string username, ShiftRegisterParams @params);
    }
}
