using BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IMessageService
    {
        MessageDTO CreateMessage(string content, int participantId);
        IEnumerable<MessageDTO> GetChatMessages(int chatId);
        MessageDTO GetMessage(int messageId);
        bool DeleteMessage(int messageId, int currentUserId);
    }
}
