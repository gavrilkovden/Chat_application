using DataAccessLayer.EntityDB;
using DataAccessLayer.Repository.generic.Interfaces;
using ExceptionHandling.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.generic
{
    public class ChatRepository : BaseRepository<ChatEntity>, IChatRepository
    {
        public ChatRepository(ChatDbContext context) : base(context)
        {
        }
        public ChatEntity CreateChat(string name, int userId)
        {
            if (string.IsNullOrEmpty(name) || userId <= 0)
            {
                throw new ChatInvalidInputException("Invalid input parameters.");
            }

            var chatEntity = new ChatEntity
            {
                ChatName = name,
            };

            var userEntity = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (userEntity == null)
            {
                throw new ChatNotFoundException("Users not found.");
            }
            _context.Chats.Add(chatEntity);
            _context.SaveChanges();
            var participantEntity = new ParticipantsEntity
            {
                UserId = userEntity.Id,
                ChatId = chatEntity.Id,
                IsAdmin = true
            };
            _context.Participants.Add(participantEntity);
            _context.SaveChanges();

            return chatEntity;
        }

        public bool DeleteChat(int chatId, int userId)
        {
            if (chatId <= 0 || userId <= 0)
            {
                throw new ChatInvalidInputException("Invalid input parameters.");
            }

            var chatEntity = _context.Chats.FirstOrDefault(c => c.Id == chatId);

            if (chatEntity == null)
            {
                throw new ChatNotFoundException("Chats not found.");
            }

            var userEntity = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (userEntity == null)
            {
                throw new ChatNotFoundException("Users not found.");
            }

            var isUserAdmin = _context.Participants.Any(p =>
                p.ChatId == chatEntity.Id && p.UserId == userEntity.Id && p.IsAdmin);

            if (!isUserAdmin)
            {
                // The user does not have the right to delete the chat, we return false
                return false;
            }
            // find all the participants of this chat and delete them from the database
            var participantsToDelete = _context.Participants.Where(p => p.ChatId == chatEntity.Id);
            _context.Participants.RemoveRange(participantsToDelete);

            // Deleting the chat from the database
            _context.Chats.Remove(chatEntity);
            _context.SaveChanges();

            return true;
        }
    }
}
