using LoudInSight.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoudInSight.BusinessObject.Interfaces
{
    public interface ILoginManager:IBaseManager
    {
        Login Login(Login login);
    }
}
