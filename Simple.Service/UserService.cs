using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Data;

namespace Simple.Service
{
    public interface IUserService
    {
        bool IsAdmin();
    }
    public class UserService : IUserService
    {
        private readonly IUnitOfWork work;
        public UserService(IUnitOfWork work)
        {
            this.work = work;            
        }

        public bool IsAdmin()
        {
            return true;
        }
    }
}
