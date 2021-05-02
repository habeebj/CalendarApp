using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CalendarApp.Domain.Entities;
using CalendarApp.WebAPI.Models;
using CalendarApp.WebAPI.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginModel.Username);
                if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
                {
                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(TenantUserProvider.CompanyIdClaim, user.CompanyId.ToString()),
                    new Claim(TenantUserProvider.TimezoneClaim, user.Timezone)
                };

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
                    var token = new JwtSecurityToken(
                        issuer: _jwtOptions.ValidIssuer,
                        audience: _jwtOptions.ValidAudience,
                        expires: DateTime.Now.AddHours(3),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        SecurityTokenNoExpirationException = token.ValidTo
                    });

                }
            }
            return BadRequest(new { Message = "Invalid username or password", Type = "Error" });
        }
    }
}