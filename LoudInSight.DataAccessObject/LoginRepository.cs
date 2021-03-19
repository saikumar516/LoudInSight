using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LoudInSight.DataAccessObject.Interfaces;
using LoudInSight.Entities;
using MongoDB.Driver;

namespace LoudInSight.DataAccessObject
{
    public class LoginRepository: BaseRepository,ILoginRepository
    {
        private readonly IMongoCollection<Login> _Logins;

        public LoginRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _Logins = database.GetCollection<Login>(CollectionName.User.ToString());
        }

        public async Task<Login> Login(Login login)
        {
            var result = string.IsNullOrEmpty(login.Email) ?
             _Logins.Find(l => l.MobileNumber == login.MobileNumber && l.Password == login.Password).Project(x => new Login { Email = x.Email, MobileNumber = x.MobileNumber }) :
             _Logins.Find(l => l.Email == login.Email && l.Password == login.Password).Project(x => new Login { Email = x.Email, MobileNumber = x.MobileNumber });
            // var aa =_Logins.Find(l => l._id == "6049a2567968cb864669468d").Project(x => new { x.Email, x.MobileNumber }).FirstOrDefault();
            // var aaaa = _Logins.Find(l => l.Email == login.Email && l.Password == login.Password).Project(x => new Login { Email = x.Email, MobileNumber = x.MobileNumber }).FirstOrDefault();
            //// var aa = _Logins.Find(l => l._id == "6049a2567968cb864669468d").FirstOrDefault();
            // var tt = _Logins.Find(book => true).ToList();

            return await result.FirstOrDefaultAsync();
        }
        //public Login Get(string id) =>
        //    _Logins.Find<Login>(Login => Login.Id == id).FirstOrDefault();

        //public Login Create(Login Login)
        //{
        //    _Logins.InsertOne(Login);
        //    return Login;
        //}

        //public void Update(string id, Login LoginIn) =>
        //    _Logins.ReplaceOne(Login => Login.Id == id, LoginIn);

        //public void Remove(Login LoginIn) =>
        //    _Logins.DeleteOne(Login => Login.Id == LoginIn.Id);

        //public void Remove(string id) =>
        //    _Logins.DeleteOne(Login => Login.Id == id);
    }
}
