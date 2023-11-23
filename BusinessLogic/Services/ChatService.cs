using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using DataAccessLayer.EntityDB;
using DataAccessLayer.Repository.generic;
using DataAccessLayer.Repository.generic.Interfaces;

namespace BusinessLogic.Services
{
    public class ChatService: IChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public ChatDTO CreateChat(string name, int userId)
        {

            var chatEntity = _chatRepository.CreateChat(name, userId);
            var chatDTO = new ChatDTO()
            {
                Id = chatEntity.Id,
                ChatName = chatEntity.ChatName
            };
            return chatDTO;
        }

        public bool DeleteChat(int chatId, int userId)
        {
            return _chatRepository.DeleteChat(chatId,userId); 
        }

        public IEnumerable<ChatDTO> SearchChats(string query)
        {
            //search for chats by query in the database
            var chats = _chatRepository.GetAll(c => c.ChatName.Contains(query));

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
            var chats = _chatRepository.GetAll();

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