using LoudInSight.DataAccessObject.Interfaces;
using LoudInSight.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoudInSight.DataAccessObject
{
    public class AccountRepository: BaseRepository,IAccountRepository
    {
        
        private readonly IMongoCollection<UserRegistration> _userRegistration;
       
        public AccountRepository(IDatabaseSettings _settings)
        {
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _userRegistration = database.GetCollection<UserRegistration>(CollectionName.UserTable.ToString());
        }
        public bool Register(UserRegistration userRegistration)
        {
            _userRegistration.InsertOne(userRegistration);
            return true;
        }
    }
}
