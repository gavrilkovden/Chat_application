using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chat_application.Controllers
{
    [ApiController]
    [Route("api/messages")] 
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService; // Service interface for working with messages

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        // Adding a new message to the chat
        [HttpPost]
        public ActionResult<MessageDTO> CreateMessage(string content, int participantIdt)
        {
                var createdMessage = _messageService.CreateMessage(content, participantIdt);
                return Ok(createdMessage);
        }

        // Receiving all chat messages
        [HttpGet("chat/{chatId}")]
        public ActionResult<IEnumerable<MessageDTO>> GetChatMessages(int chatId)
        {
                var messages = _messageService.GetChatMessages(chatId);
                return Ok(messages);
        }

        // Receiving a message by its ID
        [HttpGet("{messageId}")]
        public ActionResult<MessageDTO> GetMessage(int messageId)
        {
                var message = _messageService.GetMessage(messageId);
                return Ok(message);
        }

        // Deleting a message by its ID
        [HttpDelete("{messageId}")]
        public IActionResult DeleteMessage(int messageId, int participantId)
        {
                var result = _messageService.DeleteMessage(messageId, participantId);
                if (!result)
                {
                    return NotFound("Messages not found or you don't have permission to delete it.");
                }
                return NoContent();
        }
    }
}
