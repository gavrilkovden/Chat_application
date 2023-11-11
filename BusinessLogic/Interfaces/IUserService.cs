using BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IUserService
    {
        UserDTO CreateUser(string name);
        bool DeleteUser(int userId);
        IEnumerable<UserDTO> GetAllUsers();
        UserDTO GetUserById(int userId);
    }
}
