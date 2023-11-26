using DataAccessLayer.EntityDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.generic.Interfaces
{
    public interface IChatRepository : IRepository<ChatEntity>
    {
        ChatEntity CreateChat(string name, int userId);
        bool DeleteChat(int chatId, int userId);
      //  IEnumerable<ChatEntity> SearchChats(string query);

    }
}
