using LoudInSight.DataAccessObject.Interfaces;
using LoudInSight.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoudInSight.DataAccessObject
{
    public class AccountRepository: BaseRepository,IAccountRepository
    {
        
        private readonly IMongoCollection<UserRegistration> _userRegistration;
       
        public AccountRepository(IDatabaseSettings _settings)
        {
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _userRegistration = database.GetCollection<UserRegistration>(CollectionName.User.ToString());
        }
        public async Task<UserRegistration> Register(UserRegistration userRegistration)
        {
			try
			{
				await _userRegistration.InsertOneAsync(userRegistration);
				return userRegistration;
			}
			catch (Exception)
			{

				throw;
			}
        }
        public async Task<bool> EmailExistsWithOtherUser(UserRegistration userRegistration)
        {
			try
			{
				return await _userRegistration.FindAsync(l => l.Email == userRegistration.Email).Result.AnyAsync();
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
				return await _userRegistration.FindAsync(l => l.MobileNumber == userRegistration.MobileNumber).Result.AnyAsync();
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
