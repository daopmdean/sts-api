using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

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
