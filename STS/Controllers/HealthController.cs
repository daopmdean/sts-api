using Microsoft.AspNetCore.Mvc;
using Service.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace STS.Controllers
{
    public class HealthController : ApiBaseController
    {
        [HttpGet]
        public ActionResult<HeathStatus> Get()
        {
            HeathStatus status = new HeathStatus
            {
                Message = "OK",
                DatabaseConnection = "OK"
            };
            return Ok(status);
        }
    }
}
