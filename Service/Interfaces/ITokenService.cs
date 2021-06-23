using System.Threading.Tasks;
using Data.Entities;

namespace Service.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user);
    }
}
