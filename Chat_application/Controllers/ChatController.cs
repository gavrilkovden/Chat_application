using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chat_application.Controllers
{
    [ApiController]
    [Route("api/chats")] 
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService; // Service interface for working with chats

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        public ActionResult<ChatDTO> CreateChat(string name, int userId)
        {
                var chatDTO = _chatService.CreateChat(name, userId);
                return Ok(chatDTO); 
        }

        [HttpDelete]
        public IActionResult DeleteChat(int chatId, int userId)
        {
                bool success = _chatService.DeleteChat(chatId, userId);
                if (success)
                {
                    return Ok(); 
                }
                else
                {
                    return BadRequest("There are no permissions to do the operation"); 
                }
        }

        [HttpGet("search/{query}")]
        public ActionResult<IEnumerable<ChatDTO>> SearchChats(string query)
        {
                var chatDTOs = _chatService.SearchChats(query);
                return Ok(chatDTOs); 
         }

        [HttpGet]
        public ActionResult<IEnumerable<ChatDTO>> GetAllChats()
        {
                var chatDTOs = _chatService.GetAllChats();
                return Ok(chatDTOs); 
        }
    }
}
