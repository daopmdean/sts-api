using System;
using System.Threading.Tasks;
using Data.Models.Requests;
using Data.Repositories.Interfaces;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepo;

        public AdminService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task AssignBrand(BrandAssign brandAssign)
        {
            var user = await _userRepo
                .GetUserByUsernameAsync(brandAssign.Username);

            user.BrandId = brandAssign.BrandId;
            _userRepo.Update(user);

            if (await _userRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not assign brand");
        }
    }
}
