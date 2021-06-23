using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace STS.Controllers
{
    [Authorize]
    [Route("api/reports")]
    public class ReportsController : ApiBaseController
    {
        private readonly IManagerService _managerService;

        public ReportsController(IManagerService managerService)
        {
            _managerService = managerService;
        }
    }
}
