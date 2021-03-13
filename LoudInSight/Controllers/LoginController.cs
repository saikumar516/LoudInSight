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
using Microsoft.IdentityModel.Tokens;

namespace LoudInSight.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly ILoginManager loginManager;
        public LoginController(IDatabaseSettings settings,ILoginManager _loginService)
        {
            loginManager = _loginService;
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(Login login)
        {
            var passwordHasher = new PasswordHasher<Login>();
            string password = login.Password;
            var hasedPassword = passwordHasher.HashPassword(login, login.Password);
            login.Password = hasedPassword;
            var user =  loginManager.Login(login);
            if (user != null && passwordHasher.VerifyHashedPassword(user, hasedPassword, password) == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success)
            {
                
                //if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                //{
                //    return LocalRedirect(ReturnUrl);
                //}
                //return RedirectToAction("index", "home");

                var authClaims = new List<Claim> {
                        new Claim(ClaimTypes.Name,user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                    };
                var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretApptest12348576"));
                var token = new JwtSecurityToken(
                    issuer: "http://localhost:63789",
                    audience: "User",
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
