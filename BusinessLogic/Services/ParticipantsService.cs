using BusinessLogic.DTO;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using DataAccessLayer.EntityDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ParticipantsService : IParticipantsService
    {
        private readonly Context _context; 

        public ParticipantsService(Context context)
        {
            _context = context;
        }

        public IEnumerable<ParticipantsDTO> GetChatParticipants(int chatId)
        {
            if (chatId <= 0)
            {
                throw new ArgumentException("Invalid input parameters.");
            }
            var participants = _context.DALParticipants
                .Where(p => p.ChatId == chatId)
                .Select(p => new ParticipantsDTO
                {
                    UserId = p.UserId,
                    IsAdmin = p.IsAdmin,
                })
                .ToList();
            if (participants == null)
            {
                throw new NotFoundException("Participants not found");
            }
                return participants;
        }
    }
}
