using LoudInSight.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoudInSight.DataAccessObject.Interfaces
{
    public interface ILoginRepository:IBaseRepository
    {
        Task<Login> Login(Login login);
    }
}
