using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoudInSight.BusinessObject.Interfaces;
using LoudInSight.DataAccessObject.Interfaces;
using LoudInSight.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LoudInSight.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManager accountManager;

        public IDatabaseSettings settings { get; }
        public AccountController(IDatabaseSettings _settings, IAccountManager _accountServie)
        {
            settings = _settings;
            accountManager = _accountServie;
        }

        

        [HttpPost]
        //[AllowAnonymous]
        [Route("RegisterUser")]
        public string Register(UserRegistration userRegistration)
        {
            accountManager.Register(userRegistration);
            return "Register";
        }
    }
}
