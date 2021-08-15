using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace STS.Controllers
{
    [Authorize]
    [Route("api/schedule")]
    public class SchedulingController : ApiBaseController
    {
        private readonly IScheduleService _scheduleService;

        public SchedulingController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }
    }
}
