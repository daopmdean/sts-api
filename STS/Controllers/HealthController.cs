using Data.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace STS.Controllers
{
    [Route("api/health")]
    public class HealthController : ApiBaseController
    {
        public HealthController()
        {
        }

        [HttpGet]
        public ActionResult<HeathStatus> Get()
        {
            HeathStatus status = new()
            {
                Message = "OK",
                DatabaseConnection = "OK"
            };
            return Ok(status);
        }

        [HttpGet("algorithm")]
        public async Task<ActionResult> GetFromAlgorithm()
        {
            HttpClient client = new();
            //var responseString = await client
            //    .GetStringAsync("http://localhost:8070/api/scheduling");
            var responseString = await client
                .GetStringAsync("https://localhost:44354/api/scheduling");

            return Ok(responseString);
        }
    }
}
