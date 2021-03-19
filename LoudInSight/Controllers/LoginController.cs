using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LoudInSight.BusinessObject.Interfaces;
using LoudInSight.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace LoudInSight.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
		private readonly IDatabaseSettings settings;
		private readonly ILoginManager loginManager;
		private readonly IConfiguration configuration;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IDatabaseSettings _settings,ILoginManager _loginService, IConfiguration _configuration,ILogger<LoginController> logger)
        {
			this.settings = _settings;
			loginManager = _loginService;
			this.configuration = _configuration;
            _logger = logger;
		}
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(Login login)
        {
            var passwordHasher = new PasswordHasher<Login>();
            string password = login.Password;
            var hasedPassword = passwordHasher.HashPassword(login, login.Password);
            login.Password = hasedPassword;
            var user = await loginManager.Login(login);
            if (user != null && passwordHasher.VerifyHashedPassword(user, hasedPassword, password) == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success)
            {
                
                //if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                //{
                //    return LocalRedirect(ReturnUrl);
                //}
                //return RedirectToAction("index", "home");

                var authClaims = new List<Claim> {
                        new Claim(ClaimTypes.Name,user.Email,hasedPassword,configuration["JWT:ValidIssue"],configuration["JWT:ValidAudience"]),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                    };
                var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: configuration["JWT:ValidIssue"],
                    audience: configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(30),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            else
            {
                return Unauthorized(user);
            }
            
        }
    }

}
