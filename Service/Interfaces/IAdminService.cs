using System;
using System.Threading.Tasks;
using Data.Models.Requests;

namespace Service.Interfaces
{
    public interface IAdminService
    {
        Task AssignBrand(BrandAssign brandAssign);
    }
}
