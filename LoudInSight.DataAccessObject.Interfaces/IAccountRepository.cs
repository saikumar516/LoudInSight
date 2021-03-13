using LoudInSight.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoudInSight.DataAccessObject.Interfaces
{
    public interface IAccountRepository:IBaseRepository
    {
        bool Register(UserRegistration userRegistration);
    }
}
