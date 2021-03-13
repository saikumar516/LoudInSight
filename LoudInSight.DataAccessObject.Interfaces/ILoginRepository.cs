using LoudInSight.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoudInSight.DataAccessObject.Interfaces
{
    public interface ILoginRepository:IBaseRepository
    {
        Login Login(Login login);
    }
}
