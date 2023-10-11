using BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IChatService
    {
        ChatDTO CreateChat(string name, int userId);
        ChatDTO ConnectToChat(int chatId, int userId);
        public bool LeaveChat(int chatId, int userId);
        bool DeleteChat(int chatId, int userId);
        IEnumerable<ChatDTO> SearchChats(string query);
        IEnumerable<ChatDTO> GetAllChats();
    }
}
