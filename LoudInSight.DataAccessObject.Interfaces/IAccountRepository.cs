using LoudInSight.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoudInSight.DataAccessObject.Interfaces
{
    public interface IAccountRepository:IBaseRepository
    {
        Task<UserRegistration> Register(UserRegistration userRegistration);
        Task<bool> EmailExistsWithOtherUser(UserRegistration userRegistration);
        Task<bool> MobileNumberExistsWithOtherUser(UserRegistration userRegistration);
    }
}
