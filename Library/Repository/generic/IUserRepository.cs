using DataAccessLayer.EntityDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.generic
{
    public interface IUserRepository: IRepository<UserEntity>
    {
        bool DeleteUser(int userId);
    }
}
