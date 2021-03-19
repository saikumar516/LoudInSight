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
using Microsoft.Extensions.Logging;

namespace LoudInSight.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManager accountManager;
        private readonly ILogger<AccountController> _logger;


        public IDatabaseSettings settings { get; }
        public AccountController(IDatabaseSettings _settings, IAccountManager _accountServie,ILogger<AccountController> logger)
        {
            settings = _settings;
            accountManager = _accountServie;
            _logger = logger;
        }

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<UserRegistration> Register(UserRegistration userRegistration)
        {
			try
			{
				var passwordHasher = new PasswordHasher<UserRegistration>();
				var hasedPassword = passwordHasher.HashPassword(userRegistration, userRegistration.Password);
				userRegistration.Password = hasedPassword;
				return await accountManager.Register(userRegistration);
			}
			catch (Exception ex)
			{

				throw ex;
			}
            
        }
    }
}
