using LoudInSight.BusinessObject.Interfaces;
using LoudInSight.DataAccessObject.Interfaces;
using LoudInSight.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<bool> EmailExistsWithOtherUser(UserRegistration userRegistration)
		{
			try
			{
				return await AccountDAO.EmailExistsWithOtherUser(userRegistration);
			}
			catch (Exception)
			{

				throw;
			}
        }
        public async Task<bool> MobileNumberExistsWithOtherUser(UserRegistration userRegistration)
		{
			try
			{
				return await AccountDAO.MobileNumberExistsWithOtherUser(userRegistration);
			}
			catch (Exception)
			{

				throw;
			}
        }
        public async Task<UserRegistration> Register(UserRegistration userRegistration)
        {
			try
			{
				if (await EmailExistsWithOtherUser(userRegistration))
				{
					userRegistration.ErrorCode = ErrorCode.UserRegisterWithEmail;
					userRegistration.ErrorMessage.Add("Email is already register with other user.");
					return userRegistration;
				}
				if (await MobileNumberExistsWithOtherUser(userRegistration))
				{
					userRegistration.ErrorCode = ErrorCode.UserRegisteredWithMobileNumber;
					userRegistration.ErrorMessage.Add("Mobile Number is already register with other user.");
					return userRegistration;
				}

				await AccountDAO.Register(userRegistration);
				return userRegistration;
			}
			catch (Exception ex)
			{

				throw;
			}
        }
    }
}
