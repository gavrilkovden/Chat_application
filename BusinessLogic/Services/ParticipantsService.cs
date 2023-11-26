using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using DataAccessLayer.EntityDB;
using DataAccessLayer.Repository.generic;
using DataAccessLayer.Repository.generic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BusinessLogic.Services
{
    public class ParticipantsService : IParticipantsService
    {
        private readonly IParticipantsRepository _participantsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ParticipantsService(IParticipantsRepository participantsRepository, IUnitOfWork unitOfWork)
        {
            _participantsRepository = participantsRepository;
            _unitOfWork = unitOfWork;
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
            var participant = _participantsRepository.Create(chatId, userId);

            var participantEntityDTO = new ParticipantsDTO
            {
                UserId = participant.UserId,
                ChatId = participant.ChatId
            };

            // Save changes in the unit of work
            _unitOfWork.SaveChanges();

            return participantEntityDTO;
        }

        public bool LeaveChat(int chatId, int userId)
        {
            var isUserAdmin = _participantsRepository.IsUserAdmin(chatId, userId);

            if (isUserAdmin)
            {
                return false;
            }

            var result = _participantsRepository.Delete(chatId, userId);

            // Save changes in the unit of work
            _unitOfWork.SaveChanges();

            return result;
        }
    }

}
