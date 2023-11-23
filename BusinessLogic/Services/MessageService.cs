using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using DataAccessLayer.EntityDB;
using DataAccessLayer.Repository.generic;
using DataAccessLayer.Repository.generic.Interfaces;

namespace BusinessLogic.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public MessageDTO CreateMessage(string content, int participantId)
        {
            var messageEntity = _messageRepository.Create(new MessageEntity { Content = content, ParticipantId = participantId });

            var messageDTO = new MessageDTO
            {
                Id = messageEntity.Id,
                Content = messageEntity.Content,
                Timestamp = messageEntity.Timestamp,
                ParticipantId = messageEntity.ParticipantId
            };

            return messageDTO;
        }

        public MessageDTO GetMessage(int messageId)
        {
            
            var messageEntity = _messageRepository.GetById(messageId);

            var messageDTO = new MessageDTO
            {
                Id = messageEntity.Id,
                Content = messageEntity.Content,
                Timestamp = messageEntity.Timestamp,
                ParticipantId = messageEntity.ParticipantId
            };

            return messageDTO;
        }

        public IEnumerable<MessageDTO> GetChatMessages(int chatId)
        {

            var messages = _messageRepository.GetAll(p => p.Participant.ChatId == chatId).OrderBy(m => m.Timestamp).ToList();

            var messageDTOs = messages.Select(messageEntity => new MessageDTO
            {
                Id = messageEntity.Id,
                Content = messageEntity.Content,
                Timestamp = messageEntity.Timestamp,
                ParticipantId = messageEntity.ParticipantId
            });

            return messageDTOs;
        }

        public bool DeleteMessage(int messageId, int participantId)
        {
            return _messageRepository.DeleteMessage(messageId, participantId);
        }
    }
}
