using DataAccessLayer.EntityDB;
using ExceptionHandling.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.generic
{
    public class ParticipantsRepository : BaseRepository<ParticipantsEntity>, IParticipantsRepository
    {
        public ParticipantsRepository(ChatDbContext context) : base(context)
        {
        }

        public ParticipantsEntity ConnectToChat(int chatId, int userId)
        {
            if (chatId <= 0 || userId <= 0)
            {
                throw new ChatInvalidInputException("Invalid input parameters.");
            }
            // Search for a user in the database by his ID
            var userEntity = _context.Users.FirstOrDefault(u => u.Id == userId);

            // Search for a chat in the database by its ID
            var chatEntity = _context.Chats.FirstOrDefault(c => c.Id == chatId);

            // Checking that the user and chat exist
            if (userEntity == null || chatEntity == null)
            {
                throw new ChatNotFoundException("Users or chat not found.");
            }

            // Creating a record of the user's connection to the chat in the Participants Entity table
            var participantEntity = new ParticipantsEntity
            {
                UserId = userEntity.Id,
                ChatId = chatEntity.Id
            };

            _context.Participants.Add(participantEntity);
            _context.SaveChanges();

            return participantEntity;
        }

        public bool LeaveChat(int chatId, int userId)
        {
            if (chatId <= 0 || userId <= 0)
            {
                throw new ChatInvalidInputException("Invalid input parameters.");
            }
            // Search for a chat in the database by its ID
            var chatEntity = _context.Chats.FirstOrDefault(c => c.Id == chatId);

            // Checking that the chat exists
            if (chatEntity == null)
            {
                throw new ChatNotFoundException("Chats not found.");
            }

            // Search for a user in the database by his ID
            var userEntity = _context.Users.FirstOrDefault(u => u.Id == userId);

            // Verifying that the user exists
            if (userEntity == null)
            {
                throw new ChatNotFoundException("Users not found.");
            }

            // Checking whether the user has the right to leave the chat
            var isUserAdmin = _context.Participants.Any(p =>
                p.ChatId == chatEntity.Id && p.UserId == userEntity.Id && p.IsAdmin);

            if (isUserAdmin)
            {
                // The user cannot leave the chat
                return false;
            }

            // Deleting a user record from the Participants Entity table associated with a chat
            var participantEntity = _context.Participants
                .FirstOrDefault(p => p.ChatId == chatId && p.UserId == userId);

            if (participantEntity != null)
            {
                _context.Participants.Remove(participantEntity);
                _context.SaveChanges();
            }

            return true; // The user has successfully left the chat
        }
     }
}
