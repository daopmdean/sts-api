using System;
using System.Threading.Tasks;
using Service.Models;

namespace Service.Interfaces
{
    public interface IAuthService
    {
        Task<UserReturn> Register(RegisterInfo info);
        Task<UserReturn> Login(LoginInfo info);
    }
}
