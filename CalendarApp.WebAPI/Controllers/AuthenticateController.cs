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
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestModel loginModel)
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

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(authClaims),
                        Expires = DateTime.UtcNow.AddHours(3),
                        Audience = _jwtOptions.ValidAudience,
                        Issuer = _jwtOptions.ValidIssuer,
                        SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);

                    // var _token = new JwtSecurityToken(
                    //     issuer: _jwtOptions.ValidIssuer,
                    //     audience: _jwtOptions.ValidAudience,
                    //     expires: DateTime.Now.AddHours(3),
                    //     claims: authClaims,
                    //     signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    // );

                    return Ok(new LoginResponseModel { Token = jwtToken });
                }
            }
            return BadRequest(ErrorModel.Create("Invalid username or password"));
        }
    }
}