using System.Threading.Tasks;
using Data.Models.Requests;
using Data.Models.Responses;

namespace Service.Interfaces
{
    public interface IAuthService
    {
        Task<UserResponse> Register(RegisterRequest info);
        Task<UserResponse> Login(LoginRequest info);
    }
}
