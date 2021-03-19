using LoudInSight.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoudInSight.BusinessObject.Interfaces
{
    public interface IAccountManager:IBaseManager
    {
        Task<UserRegistration> Register(UserRegistration userRegistration);
        Task<bool> EmailExistsWithOtherUser(UserRegistration userRegistration);
        Task<bool> MobileNumberExistsWithOtherUser(UserRegistration userRegistration);
    }
}
