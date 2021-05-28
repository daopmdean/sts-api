using Data.Entities;

namespace Service.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
