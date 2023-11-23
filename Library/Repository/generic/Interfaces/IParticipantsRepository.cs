using DataAccessLayer.EntityDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.generic
{
    public interface IParticipantsRepository : IRepository<ParticipantsEntity>
    {
    //    IEnumerable<ParticipantsEntity> GetChatParticipants(int chatId);
        ParticipantsEntity ConnectToChat(int chatId, int userId);
        bool LeaveChat(int chatId, int userId);
    }
}
