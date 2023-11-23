using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using DataAccessLayer.EntityDB;
using DataAccessLayer.Repository.generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BusinessLogic.Services
{
    public class ParticipantsService : IParticipantsService
    {
        private readonly IParticipantsRepository _participantsRepository;

        public ParticipantsService(IParticipantsRepository participantsRepository)
        {
            _participantsRepository = participantsRepository;
        }

        public IEnumerable<ParticipantsDTO> GetChatParticipants(int chatId)
        {
            var participants = _participantsRepository.GetAll(p => p.ChatId == chatId, p => p.Include(p => p.Messages).Include(p => p.User))
                .Select(p => new ParticipantsDTO
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    IsAdmin = p.IsAdmin,
                    ChatId = p.ChatId
                });

            return participants;
        }

        public ParticipantsDTO ConnectToChat(int chatId, int userId)
        {
            var participant = _participantsRepository.ConnectToChat(chatId, userId);

            var participantEntityDTO = new ParticipantsDTO
            {
                UserId = participant.UserId,
                ChatId = participant.ChatId
            };

            return participantEntityDTO;
        }

        public bool LeaveChat(int chatId, int userId)
        {
            return _participantsRepository.LeaveChat(chatId, userId);
        }

        //public ParticipantsDTO GetParticipantId(int id)
        //{
        //    var participant = _participantsRepository.GetMessageId(id);

        //    var participantEntityDTO = new ParticipantsDTO
        //    {
        //        UserId = participant.UserId,
        //        ChatId = participant.ChatId
        //    };

        //    return participantEntityDTO;


        
    }
}
