using System;
using System.Threading.Tasks;
using Data.Models.Requests;

namespace Service.Interfaces
{
    public interface IManagerService
    {
        Task AssignStoreManager(StoreAssign brandAssign);
        Task<StaffCreate> CreateStaff(int brandId, StaffCreate info);
    }
}
