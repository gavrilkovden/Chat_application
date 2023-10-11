using BusinessLogic.DTO;
using BusinessLogic.Exceptions;
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
        [HttpPost("create")]
        public ActionResult<MessageDTO> CreateMessage(string content, int participantIdt)
        {
            try
            {
                var createdMessage = _messageService.CreateMessage(content, participantIdt);
                return Ok(createdMessage);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Receiving all chat messages
        [HttpGet("chat/{chatId}")]
        public ActionResult<IEnumerable<MessageDTO>> GetChatMessages(int chatId)
        {
            try
            {
                var messages = _messageService.GetChatMessages(chatId);
                return Ok(messages);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Receiving a message by its ID
        [HttpGet("{messageId}")]
        public ActionResult<MessageDTO> GetMessage(int messageId)
        {
            try
            {
                var message = _messageService.GetMessage(messageId);
                return Ok(message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Deleting a message by its ID
        [HttpDelete("{messageId}")]
        public IActionResult DeleteMessage(int messageId, int participantId)
        {
            try
            {
                var result = _messageService.DeleteMessage(messageId, participantId);
                if (!result)
                {
                    return NotFound("Message not found or you don't have permission to delete it.");
                }
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
