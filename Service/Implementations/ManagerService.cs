using System;
using System.Threading.Tasks;
using Data.Models.Requests;
using Service.Interfaces;

namespace Service.Implementations
{
    public class ManagerService : IManagerService
    {
        public ManagerService()
        {
        }

        public Task AssignStoreManager(StoreAssign brandAssign)
        {
            throw new NotImplementedException();
        }
    }
}
