using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace STS.Controllers
{
    [Authorize]
    [Route("api/admin")]
    public class AdminController : ApiBaseController
    {
        private readonly IUserService _userService;
        public AdminController(IUserService userService)
        {
            _userService = userService;
        }
    }
}
