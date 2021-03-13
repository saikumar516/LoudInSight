using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoudInSight.BusinessObject.Interfaces;
using LoudInSight.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public string Login(Login login)
        {
           var _login =  loginManager.Login(login);
            return "Register";
        }
    }
}
