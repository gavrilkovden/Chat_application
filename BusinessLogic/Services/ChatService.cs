using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using DataAccessLayer.EntityDB;

namespace BusinessLogic.Services
{
    public class ChatService: IChatService
    {
        private readonly Context _context; 

        public ChatService(Context context)
        {
            _context = context;
        }

        public ChatDTO CreateChat(string name, int userId)
        {
            if (string.IsNullOrEmpty(name) || userId <= 0)
            {
               // throw new InvalidInputException("Invalid input parameters.");
            }

            var chatEntity = new ChatEntity
            {
                ChatName = name,
            };
            _context.Chat.Add(chatEntity);
            _context.SaveChanges();

            // Search for a user in the database by his ID
            var userEntity = _context.User.FirstOrDefault(u => u.Id == userId);
            if (userEntity == null)
            {
           //     throw new  NotFoundException("User not found.");
            }
            // Creating a record of the user's connection to the chat and installing it by the administrator
            var participantEntity = new ParticipantsEntity
            {
                UserId = userEntity.Id,
                ChatId = chatEntity.Id,
                IsAdmin = true 
            };

            var chatDTO = new ChatDTO()
            {
                Id = chatEntity.Id,
                ChatName = chatEntity.ChatName
            };
            return chatDTO;
        }

        public bool DeleteChat(int chatId, int userId)
        {
            if (chatId <= 0 || userId <= 0)
            {
       //         throw new InvalidInputException("Invalid input parameters.");
            }
            // Search for a chat in the database by its ID
            var chatEntity = _context.Chat.FirstOrDefault(c => c.Id == chatId);

            // Checking that the chat exists
            if (chatEntity == null)
            {
        //        throw new NotFoundException ("Chat not found.");
            }

            // Search for a user in the database by his ID
            var userEntity = _context.User.FirstOrDefault(u => u.Id == userId);

            // Verifying that the user exists
            if (userEntity == null)
            {
     //           throw new NotFoundException ("User not found.");
            }

            // Checking whether the user has the rights to delete the chat
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
            _context.Chat.Remove(chatEntity);
            _context.SaveChanges();

            return true; 
        }

        public IEnumerable<ChatDTO> SearchChats(string query)
        {
            //search for chats by query in the database
            var chats = _context.Chat.Where(c => c.ChatName.Contains(query)).ToList();

            // Creating a ChatDTO collection and filling it with found chats
            var chatDTOs = new List<ChatDTO>();

            foreach (var chatEntity in chats)
            {
                var chatDTO = new ChatDTO()
                {
                    Id = chatEntity.Id,
                    ChatName = chatEntity.ChatName
                };

                chatDTOs.Add(chatDTO);
            }

            return chatDTOs;
        }

        public IEnumerable<ChatDTO> GetAllChats()
        {
            // Getting all chats from the database
            var chats = _context.Chat.ToList();
            if (chats.Count == 0)
            {
  //              throw new NotFoundException("chats not found");
            }
            // Create a ChatDTO collection and fill it with data from all chats
            var chatDTOs = new List<ChatDTO>();

            foreach (var chatEntity in chats)
            {
                var chatDTO = new ChatDTO()
                {
                    Id = chatEntity.Id,
                    ChatName = chatEntity.ChatName
                };
                chatDTOs.Add(chatDTO);
            }

            return chatDTOs;
        }
    }

}