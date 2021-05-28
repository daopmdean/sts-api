using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models.Responses;

namespace Service.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserOverview>> GetUserOverviews();

    }
}
