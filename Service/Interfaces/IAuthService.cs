using System.Threading.Tasks;
using Service.Models.Requests;
using Service.Models.Responses;

namespace Service.Interfaces
{
    public interface IAuthService
    {
        Task<UserResponse> Register(RegisterRequest info);
        Task<UserResponse> Login(LoginRequest info);
    }
}
