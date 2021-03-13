using LoudInSight.BusinessObject.Interfaces;
using LoudInSight.DataAccessObject.Interfaces;
using LoudInSight.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoudInSight.BusinessObject
{
    public class AccountManager : BaseManager,IAccountManager
    {
        private IAccountRepository AccountDAO { get; }
        public IDatabaseSettings Settings { get; }
        public AccountManager(IDatabaseSettings _settings, IAccountRepository _accountDAO)//:base(_settings,_accountDAO)
        {
            Settings = _settings;
            AccountDAO = _accountDAO;
        }
        public bool Register(UserRegistration userRegistration)
        {
            return AccountDAO.Register(userRegistration);
        }
    }
}
