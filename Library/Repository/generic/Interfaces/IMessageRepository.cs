using DataAccessLayer.EntityDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.generic
{
    public interface IMessageRepository : IRepository<MessageEntity>
    {
     //   IEnumerable<MessageEntity> GetMessageChatById (int chatId);
        bool DeleteMessage(int messageId, int currentUserId);
    }
}
