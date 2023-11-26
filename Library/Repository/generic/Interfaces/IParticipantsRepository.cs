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
        ParticipantsEntity Create(int chatId, int userId);
        bool Delete(int chatId, int userId);
        bool IsUserAdmin(int chatId, int userId);
    }
}
