
using LoudInSight.BusinessObject.Interfaces;
using LoudInSight.DataAccessObject.Interfaces;
using LoudInSight.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoudInSight.BusinessObject
{
    public class LoginManager: BaseManager, ILoginManager
    {
        private readonly IDatabaseSettings settings;
        private readonly ILoginRepository loginDAO;
        public LoginManager(IDatabaseSettings _settings, ILoginRepository _loginDAO) //: base(_settings, _loginDAO)
        {
            settings = _settings;
            loginDAO = _loginDAO;
        }

        public Login Login(Login login)
        {
           return loginDAO.Login(login);
        }
    }
}
