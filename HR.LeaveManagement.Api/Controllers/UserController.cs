using HR.LeaveManagement.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public UserController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [HttpGet]
        [Route("GetToken")]
        public ActionResult GetToken(string username)
        {
            var jwtService = new JwtService(configuration);
            var Token = jwtService.GenerateToken(username);
            return Ok(Token);
        }

    }
}
