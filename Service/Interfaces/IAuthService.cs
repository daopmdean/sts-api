using System.Threading.Tasks;
using Data.Models.Requests;
using Data.Models.Responses;

namespace Service.Interfaces
{
    public interface IAuthService
    {
        Task<UserTokenResponse> Register(RegisterRequest info);
        Task<UserTokenResponse> Login(LoginRequest info);
    }
}
