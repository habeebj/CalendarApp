using CalendarApp.Domain.Entities;
using CalendarApp.WebAPI.Models;
using CalendarApp.WebAPI.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CalendarApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWTOptions _jwtOptions;
        public AuthenticateController(IOptionsMonitor<JWTOptions> jwtOptions, UserManager<ApplicationUser> usermanager)
        {
            _userManager = usermanager;
            _jwtOptions = jwtOptions.CurrentValue;
        }

        [HttpPost]
        [Route("Login")]
        public void Login([FromBody] LoginModel loginModel)
        {
        }
    }
}