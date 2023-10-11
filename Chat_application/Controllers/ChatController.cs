using Azure.Core;
using BusinessLogic.DTO;
using BusinessLogic.Exceptions;
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

        // POST: api/chats/create
        [HttpPost("create")]
        public ActionResult<ChatDTO> CreateChat(string name, int userId)
        {
            try
            {
                var chatDTO = _chatService.CreateChat(name, userId);
                return Ok(chatDTO); 
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(404,ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/chats/connect
        [HttpPost("connect")]
        public ActionResult<ChatDTO> ConnectToChat(int chatId, int userId)
        {
            try
            {
                var chatDTO = _chatService.ConnectToChat(chatId, userId);
                return Ok(chatDTO); // Returning a successful result with chat data
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

        [HttpPut("disconnect")]
        public IActionResult DisconnectingfromСhat(int chatId, int userId)
        {
            try
            {
                bool success = _chatService.LeaveChat(chatId, userId);
                if (success)
                {
                    return Ok(); // Return 200 OK on successful shutdown
                }
                else
                {
                    return BadRequest("Unable to disconnect from the chat."); // We return an error when disconnecting unsuccessfully
                }
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

        // DELETE: api/chats/delete/{chatId}/{userId}
        [HttpDelete("delete/{chatId}/{userId}")]
        public IActionResult DeleteChat(int chatId, int userId)
        {
            try
            {
                bool success = _chatService.DeleteChat(chatId, userId);
                if (success)
                {
                    return Ok(); // We return 200 OK when the chat is successfully deleted
                }
                else
                {
                    return BadRequest("There are no permissions to do the operation"); 
                }
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

        // GET: api/chats/search/{query}
        [HttpGet("search/{query}")]
        public ActionResult<IEnumerable<ChatDTO>> SearchChats(string query)
        {
            try
            {
                var chatDTOs = _chatService.SearchChats(query);
                return Ok(chatDTOs); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<ChatDTO>> GetAllChats()
        {
            try
            {
                var chatDTOs = _chatService.GetAllChats();
                return Ok(chatDTOs); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
