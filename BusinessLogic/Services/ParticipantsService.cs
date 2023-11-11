using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using DataAccessLayer.EntityDB;

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
        //        throw new InvalidInputException("Invalid input parameters.");
            }
            var participants = _context.Participants
                .Where(p => p.ChatId == chatId)
                .Select(p => new ParticipantsDTO
                {
                    UserId = p.UserId,
                    IsAdmin = p.IsAdmin,
                })
                .ToList();
            if (participants == null)
            {
            //    throw new NotFoundException("Participants not found");
            }
                return participants;
        }

        public ParticipantsDTO ConnectToChat(int chatId, int userId)
        {
            if (chatId <= 0 || userId <= 0)
            {
        //        throw new InvalidInputException("Invalid input parameters.");
            }
            // Search for a user in the database by his ID
            var userEntity = _context.User.FirstOrDefault(u => u.Id == userId);

            // Search for a chat in the database by its ID
            var chatEntity = _context.Chat.FirstOrDefault(c => c.Id == chatId);

            // Checking that the user and chat exist
            if (userEntity == null || chatEntity == null)
            {
            //    throw new NotFoundException("User or chat not found.");
            }

            // Creating a record of the user's connection to the chat in the Participants Entity table
            var participantEntity = new ParticipantsEntity
            {
                UserId = userEntity.Id,
                ChatId = chatEntity.Id
            };

            // Adding an entry to the context and saving the changes
            _context.Participants.Add(participantEntity);
            _context.SaveChanges();

            var participantEntityDTO = new ParticipantsDTO
            {
                UserId = userEntity.Id,
                ChatId = chatEntity.Id
            };

            return participantEntityDTO;
        }

        public bool LeaveChat(int chatId, int userId)
        {
            if (chatId <= 0 || userId <= 0)
            {
             //   throw new InvalidInputException("Invalid input parameters.");
            }
            // Search for a chat in the database by its ID
            var chatEntity = _context.Chat.FirstOrDefault(c => c.Id == chatId);

            // Checking that the chat exists
            if (chatEntity == null)
            {
           //    throw new NotFoundException("Chat not found.");
            }

            // Search for a user in the database by his ID
            var userEntity = _context.User.FirstOrDefault(u => u.Id == userId);

            // Verifying that the user exists
            if (userEntity == null)
            {
           //     throw new NotFoundException("User not found.");
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
