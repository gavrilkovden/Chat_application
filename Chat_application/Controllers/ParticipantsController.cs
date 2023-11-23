using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using Chat_application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chat_application.Controllers
{
    [ApiController]
    [Route("api/participants")]
    public class ParticipantsController : ControllerBase
    {
        private readonly IParticipantsService _participantsService;

        public ParticipantsController(IParticipantsService participantsService)
        {
            _participantsService = participantsService;
        }

        [HttpPost]
        public ActionResult<ChatDTO> ConnectToChat(int chatId, int userId)
        {
            var chatDTO = _participantsService.ConnectToChat(chatId, userId);
            return Ok(chatDTO); 
        }

        [HttpGet("chat/{chatId}")]
        public IActionResult GetChatParticipants(int chatId)
        {
                var participants = _participantsService.GetChatParticipants(chatId);
                return Ok(participants);
        }

        [HttpDelete]
        public IActionResult DisconnectingfromСhat(int chatId, int userId)
        {
            bool success = _participantsService.LeaveChat(chatId, userId);
            if (success)
            {
                return Ok(); 
            }
            else
            {
                return BadRequest("Unable to disconnect from the chat."); 
            }
        }
    }
}
