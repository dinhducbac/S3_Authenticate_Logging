using EmployeeManagerment.Models;
using EmployeeManagerment.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagerment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService UserService;
        public readonly ILogger Logger;
        public readonly IConfiguration Configuration;
        public UserController(IUserService userService, ILogger<UserController> logger, IConfiguration configuration)
        {
            UserService = userService;
            Logger = logger;
            Configuration = configuration;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await UserService.Login(request);
            if (result.Success == false)
                return BadRequest(result.Message);
            var userPricipal = this.ValidateToken(result.ResultObject.Token);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = false
            };
            try
            {
                HttpContext.Session.SetString("Token", result.ResultObject.Token);
                await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                       userPricipal,
                        authProperties);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
            return Ok(result);
        }
        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;
            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();
            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = Configuration["Token:Issuer"];
            validationParameters.ValidIssuer = Configuration["Token:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"]));
            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);
            return principal;
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await UserService.Register(request);
            if (!result.Success)
                return BadRequest(result.ResultObject);
            return Ok(result.ResultObject);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

    }
}
