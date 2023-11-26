using DataAccessLayer.EntityDB;
using DataAccessLayer.Repository.generic.Interfaces;
using ExceptionHandling.Exceptions;

namespace DataAccessLayer.Repository.generic
{
    public class MessageRepository : BaseRepository<MessageEntity>, IMessageRepository
    {
        public MessageRepository(ChatDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }
        //public IEnumerable<MessageEntity> GetMessageChatById(int chatId)
        //{
        //    if (chatId <= 0)
        //    {
        //        throw new ChatInvalidInputException("Invalid input parameters.");
        //    }

        //    var messages = _context.Participants
        //        .Where(p => p.ChatId == chatId)
        //        .SelectMany(p => p.Messages)
        //        .OrderBy(m => m.Timestamp)
        //        .ToList();

        //    if (messages == null || !messages.Any())
        //    {
        //        throw new ChatNotFoundException("Messages not found.");
        //    }

        //    return messages;
        //}

        public bool DeleteMessage(int messageId, int participantId)
        {
            if (messageId <= 0 || participantId <= 0)
            {
                throw new ChatInvalidInputException("Invalid input parameters.");
            }

            var messageEntity = _context.Messages.FirstOrDefault(m => m.Id == messageId);

            if (messageEntity == null)
            {
                return false;
            }

            if (messageEntity.ParticipantId != participantId)
            {
                return false;
            }

            _context.Messages.Remove(messageEntity);
            _context.SaveChanges();

            return true;
        }
    }
}
