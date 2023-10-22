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
    public class MessageService : IMessageService
    {
        private readonly Context _context;

        public MessageService(Context context)
        {
            _context = context;
        }

        public MessageDTO CreateMessage(string content, int participantId)
        {
            if (string.IsNullOrEmpty(content) || participantId <= 0)
            {
                throw new InvalidInputException("Invalid input parameters.");
            }
            var messageEntity = new MessageEntity
            {
                Content = content,
                ParticipantId = participantId 
            };

            _context.DALMessage.Add(messageEntity);
            _context.SaveChanges();

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
            if (messageId <= 0)
            {
                throw new InvalidInputException("Invalid input parameters.");
            }
            //  receive a message with the specified identifier
            var messageEntity = _context.DALMessage.FirstOrDefault(m => m.Id == messageId);

            if (messageEntity == null)
            {
                throw new NotFoundException("Message not found");
            }

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
            if (chatId <= 0)
            {
                throw new InvalidInputException("Invalid input parameters.");
            }
            var messages = _context.DALParticipants
                .Where(p => p.ChatId == chatId)
                .SelectMany(p => p.Messages)
                .OrderBy(m => m.Timestamp)
                .ToList();

            if (messages == null)
            {
                throw new NotFoundException("Messages not found");
            }
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
            if (messageId <= 0 || participantId <= 0)
            {
                throw new InvalidInputException("Invalid input parameters.");
            }
            // search for a message with the specified ID
            var messageEntity = _context.DALMessage.FirstOrDefault(m => m.Id == messageId);

            if (messageEntity == null)
            {
                return false;
            }

            // checking that the current user has the right to delete the message (only the one who wrote it)
            if (messageEntity.ParticipantId != participantId)
            {
                return false;
            }
            _context.DALMessage.Remove(messageEntity);
            _context.SaveChanges();

            return true;
        }
    }
}
