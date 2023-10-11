using BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IParticipantsService
    {
        IEnumerable<ParticipantsDTO> GetChatParticipants(int chatId);
    }
}
