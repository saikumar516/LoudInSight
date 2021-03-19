using LoudInSight.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoudInSight.BusinessObject.Interfaces
{
    public interface ILoginManager:IBaseManager
    {
       Task<Login> Login(Login login);
    }
}
